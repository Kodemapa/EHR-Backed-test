using EHR_Application.Application.Dtos;
using EHR_Application.Application.Mappings;
using EHR_Application.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace EHR_Application.Infrastructure.Services;

public sealed class PrescriptionService : IPrescriptionService
{
    private readonly IPrescriptionRepository _prescriptionRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<PrescriptionService> _logger;

    public PrescriptionService(
        IPrescriptionRepository prescriptionRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        ILogger<PrescriptionService> logger)
    {
        _prescriptionRepository = prescriptionRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<PrescriptionDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var prescriptions = await _prescriptionRepository.GetAllAsync(cancellationToken);
        return prescriptions.Select(p => p.ToDto());
    }

    public async Task<PrescriptionViewDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionRepository.GetByIdAsync(id, cancellationToken);
        return prescription?.ToViewDto();
    }

    public async Task<PrescriptionDto> CreateAsync(CreatePrescriptionDto dto, CancellationToken cancellationToken)
    {
        // 1. Resolve Patient
        var patient = await _patientRepository.GetByMRNAsync(dto.PatientId, cancellationToken);
        if (patient == null)
        {
            if (Guid.TryParse(dto.PatientId, out var patientGuid))
            {
                patient = await _patientRepository.GetByIdAsync(patientGuid, cancellationToken);
            }
        }
        
        if (patient == null)
        {
             _logger.LogWarning("Patient not found with ID/MRN: {PatientId}", dto.PatientId);
             throw new Exception($"Patient not found with ID/MRN: {dto.PatientId}");
        }

        // 2. Resolve Doctor
        var doctor = _doctorRepository.GetDoctorByRegistration(dto.DoctorId);
        if (doctor == null)
        {
             if (Guid.TryParse(dto.DoctorId, out var doctorGuid))
            {
                doctor = _doctorRepository.GetDoctorById(doctorGuid);
            }
        }
        
        if (doctor == null)
        {
             _logger.LogWarning("Doctor not found with ID/Registration: {DoctorId}", dto.DoctorId);
             throw new Exception($"Doctor not found with ID/Registration: {dto.DoctorId}");
        }

        // 3. Generate Prescription Code
        var latestCode = await _prescriptionRepository.GetLatestCodeAsync(cancellationToken);
        var nextCode = GenerateNextCode(latestCode);

        // 4. Map and Save
        var entity = dto.ToEntity(patient.Id, doctor.Id, nextCode);
        var created = await _prescriptionRepository.AddAsync(entity, cancellationToken);
        
        // Reload to get navigation properties
        var checkCreated = await _prescriptionRepository.GetByIdAsync(created.Id, cancellationToken);
        return checkCreated!.ToDto();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdatePrescriptionDto dto, CancellationToken cancellationToken)
    {
        var existing = await _prescriptionRepository.GetByIdAsync(id, cancellationToken);
        if (existing == null) return false;

        try
        {
            existing.UpdateFromDto(dto);
            await _prescriptionRepository.UpdateAsync(id, cancellationToken);
            return true;
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        {
            // Record was deleted or modified by another process
            return false;
        }
    }

    public async Task<bool> CancelAsync(Guid id, CancelPrescriptionDto dto, CancellationToken cancellationToken)
    {
        var existing = await _prescriptionRepository.GetByIdAsync(id, cancellationToken);
        if (existing == null) return false;

        try
        {
            existing.Status = "CANCELLED";
            existing.CancellationReason = dto.Reason;
            await _prescriptionRepository.UpdateAsync(id, cancellationToken);
            return true;
        }
        catch (Microsoft.EntityFrameworkCore.DbUpdateConcurrencyException)
        {
            // Record was deleted or modified by another process
            return false;
        }
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _prescriptionRepository.GetByIdAsync(id, cancellationToken);
        if (existing == null) return false;

        await _prescriptionRepository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public Task<byte[]> PrintAsync(Guid id, CancellationToken cancellationToken)
    {
        return Task.FromResult(Array.Empty<byte>());
    }

    private string GenerateNextCode(string latestCode)
    {
        var parts = latestCode.Split('-');
        if (parts.Length < 3) return $"RX-{DateTime.UtcNow.Year}-001";

        if (int.TryParse(parts[2], out var num))
        {
            return $"{parts[0]}-{DateTime.UtcNow.Year}-{(num + 1).ToString("D3")}";
        }
        
        return $"RX-{DateTime.UtcNow.Year}-001";
    }
}
