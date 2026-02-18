using EHR_Application.Application.Dtos;
using EHR_Application.Application.Mappings;
using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EHR_Application.Infrastructure.Services;

public sealed class DoctorService : IDoctorService
{
    private readonly IDoctorRepository _repository;

    public DoctorService(IDoctorRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<DoctorDto>> GetAsync(CancellationToken cancellationToken)
    {
        var doctors = await _repository.QueryDoctors()
            .ToListAsync(cancellationToken); return doctors.Select(d => d.ToDto());
    }

    public async Task<DoctorDto?> GetAsync(Guid id,CancellationToken cancellationToken)
    {
        var doctor = await _repository.QueryDoctors()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        return doctor?.ToDto();
    }

    public async Task<DoctorDto> CreateAsync(CreateDoctorDto dto, CancellationToken cancellationToken)
    {
        var doctor = dto.ToEntity();

        _repository.AddDoctor(doctor);
        await SaveAsync(cancellationToken);

        return doctor.ToDto();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateDoctorDto dto, CancellationToken cancellationToken)
    {
        var doctor = await _repository.QueryDoctors()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (doctor == null) return false;

        doctor.UpdateFromDto(dto);
        await SaveAsync(cancellationToken);

        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await _repository.QueryDoctors()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);

        if (doctor == null) return false;

        try
        {
            _repository.RemoveDoctor(doctor);
            await SaveAsync(cancellationToken);
            return true;
        }
        catch (DbUpdateException ex)
        {
            if (ex.InnerException != null && ex.InnerException.Message.Contains("violates foreign key constraint"))
            {
                throw new InvalidOperationException("Cannot delete doctor because they have associated appointments or prescriptions.");
            }
            throw;
        }
    }

    public async Task<IEnumerable<DoctorDto>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken)
    {
        var doctors = await _repository.QueryDoctors()
            .Where(d => d.Specialization == specialization)
            .ToListAsync(cancellationToken);

        return doctors.Select(d => d.ToDto());
    }

    public async Task<bool> ToggleStatusAsync(Guid id, CancellationToken cancellationToken)
    {
        var doctor = await _repository.QueryDoctors()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        
        if (doctor == null) return false;

        doctor.IsActive = !doctor.IsActive;
        await SaveAsync(cancellationToken);
        
        return true;
    }

    private async Task SaveAsync(CancellationToken cancellationToken)
        => await _repository.SaveDoctorAsync(cancellationToken);

    public async Task<bool> UploadDocumentAsync(Guid id, IFormFile file, CancellationToken cancellationToken)
    {
        var doctor = await _repository.QueryDoctors()
            .FirstOrDefaultAsync(d => d.Id == id, cancellationToken);
        
        if (doctor == null) return false;

        if (file == null || file.Length == 0) return false;

        var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".doc", ".docx" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        
        if (!allowedExtensions.Contains(extension)) return false;

        var folderName = Path.Combine("wwwroot", "uploads", "doctors");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        
        if (!Directory.Exists(pathToSave))
        {
            Directory.CreateDirectory(pathToSave);
        }

        var fileName = $"{id}_{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(pathToSave, fileName);
        var dbPath = Path.Combine("uploads", "doctors", fileName).Replace("\\", "/");

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream, cancellationToken);
        }

        doctor.Document = dbPath;
        await SaveAsync(cancellationToken);

        return true;
    }
}