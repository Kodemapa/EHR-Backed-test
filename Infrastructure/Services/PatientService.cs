using System.Linq;
using System.Collections.Generic;
using EHR_Application.Application.Dtos;
using EHR_Application.Authentication;
using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using EHR_Application.Application.Mappings;
using System.IO;
using Microsoft.AspNetCore.Http;

namespace EHR_Application.Infrastructure.Services;

public sealed class PatientService : IPatientService
{
    private readonly IPatientRepository _patientRepository;
    private readonly ICurrentUserService _currentUserService;
    private readonly ILogger<PatientService> _logger;

    public PatientService(
        IPatientRepository patientRepository,
        ICurrentUserService currentUserService,
        ILogger<PatientService> logger)
    {
        _patientRepository = patientRepository;
        _currentUserService = currentUserService;
        _logger = logger;
    }

    public async Task<IEnumerable<PatientDto>> GetAsync(CancellationToken cancellationToken)
    {
        var patients = await _patientRepository.GetAllAsync(cancellationToken);
        return patients.Select(p => p.ToDto());

    }

    public async Task<PatientDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);
        return patient?.ToDto();
    }

    public async Task<PatientDto> CreateAsync(CreatePatientDto dto, CancellationToken cancellationToken)
    {
        var patient = dto.ToEntity();

        var created = await _patientRepository.AddAsync(patient, cancellationToken);
        LogChange("created", created.Id);
        return created.ToDto();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePatientDto dto, CancellationToken cancellationToken)
    {
        var existing = await _patientRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        existing.UpdateFromDto(dto);

        await _patientRepository.UpdateAsync(existing, cancellationToken);
        LogChange("updated", existing.Id);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _patientRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null)
        {
            return false;
        }

        await _patientRepository.DeleteAsync(id, cancellationToken);
        LogChange("deleted", id);
        return true;
    }

    private void LogChange(string action, Guid patientId)
    {
        var user = _currentUserService.UserName ?? _currentUserService.UserId ?? "anonymous";
        _logger.LogInformation("User {User} {Action} patient {PatientId}", user, action, patientId);
    }

    public async Task<bool> UploadDocumentAsync(Guid id, Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(id, cancellationToken);
        if (patient == null) return false;

        if (file == null || file.Length == 0) return false;

        var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();

        if (!allowedExtensions.Contains(extension)) return false;

        var folderName = Path.Combine("wwwroot", "uploads", "patients");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);

        if (!Directory.Exists(pathToSave))
        {
            Directory.CreateDirectory(pathToSave);
        }

        var fileName = $"{id}_{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(pathToSave, fileName);
        var dbPath = Path.Combine("uploads", "patients", fileName).Replace("\\", "/");

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        // Handle JSON array of documents safely
        List<string> documents;
        try
        {
            documents = string.IsNullOrWhiteSpace(patient.Documents) 
                ? new List<string>() 
                : System.Text.Json.JsonSerializer.Deserialize<List<string>>(patient.Documents) ?? new List<string>();
        }
        catch (System.Text.Json.JsonException)
        {
            // If the stored string is somehow invalid JSON, start fresh
            documents = new List<string>();
        }

        documents.Add(dbPath);
        patient.Documents = System.Text.Json.JsonSerializer.Serialize(documents);

        await _patientRepository.UpdateAsync(patient, cancellationToken);
        LogChange("uploaded document to", patient.Id);

        return true;
    }
}
