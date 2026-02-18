using EHR_Application.Application.Dtos;
using EHR_Application.Application.Mappings;
using EHR_Application.Authentication;
using EHR_Application.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;

namespace EHR_Application.Infrastructure.Services;

public sealed class AppointmentService : IAppointmentService
{
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IPatientRepository _patientRepository;
    private readonly IDoctorRepository _doctorRepository;
    private readonly ILogger<AppointmentService> _logger;

    public AppointmentService(
        IAppointmentRepository appointmentRepository,
        IPatientRepository patientRepository,
        IDoctorRepository doctorRepository,
        ILogger<AppointmentService> logger)
    {
        _appointmentRepository = appointmentRepository;
        _patientRepository = patientRepository;
        _doctorRepository = doctorRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<AppointmentViewDto>> GetAsync(CancellationToken cancellationToken)
    {
        var appointments = await _appointmentRepository.GetAllAsync(cancellationToken);
        return appointments.Select(a => a.ToViewDto());
    }

    public async Task<AppointmentViewDto?> GetAsync(Guid id, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        return appointment?.ToViewDto();
    }

    public async Task<IEnumerable<AppointmentViewDto>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken)
    {
        var all = await _appointmentRepository.GetAllAsync(cancellationToken);
        return all.Where(a => a.PatientId == patientId).Select(a => a.ToViewDto());
    }

    public async Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto, CancellationToken cancellationToken)
    {
        var patient = await _patientRepository.GetByIdAsync(dto.PatientId, cancellationToken);
        if (patient == null)
        {
            _logger.LogError("Patient with ID {PatientId} not found.", dto.PatientId);
            throw new Exception($"Patient with ID {dto.PatientId} not found.");
        }

        if (dto.DoctorId.HasValue)
        {
            var doctor = _doctorRepository.GetDoctorById(dto.DoctorId.Value);
            if (doctor == null)
            {
                _logger.LogError("Doctor with ID {DoctorId} not found.", dto.DoctorId);
                throw new Exception($"Doctor with ID {dto.DoctorId} not found.");
            }
        }

        var entity = dto.ToEntity();
        var created = await _appointmentRepository.AddAsync(entity, cancellationToken);
        return created.ToDto();
    }

    public async Task<bool> UpdateAsync(Guid id, UpdateAppointmentDto dto, CancellationToken cancellationToken)
    {
        var existing = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        // Validate Patient
        var patient = await _patientRepository.GetByIdAsync(dto.PatientId, cancellationToken);
        if (patient == null)
        {
            _logger.LogError("Patient with ID {PatientId} not found.", dto.PatientId);
            throw new Exception($"Patient with ID {dto.PatientId} not found.");
        }

        // Validate Doctor
        if (dto.DoctorId.HasValue)
        {
            var doctor = _doctorRepository.GetDoctorById(dto.DoctorId.Value);
            if (doctor == null)
            {
                _logger.LogError("Doctor with ID {DoctorId} not found.", dto.DoctorId);
                throw new Exception($"Doctor with ID {dto.DoctorId} not found.");
            }
        }

        existing.UpdateFromDto(dto);
        await _appointmentRepository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        await _appointmentRepository.DeleteAsync(id, cancellationToken);
        return true;
    }

    public async Task<AppointmentViewDto?> GetViewAsync(Guid id, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        return appointment?.ToViewDto();
    }

    public async Task<bool> RescheduleAsync(Guid id, RescheduleAppointmentDto dto, CancellationToken cancellationToken)
    {
        var existing = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        existing.DateTime = dto.NewScheduledAt;
        existing.Reschedule = "Yes"; // Or whatever logic
        await _appointmentRepository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> CancelAsync(Guid id, CancelAppointmentDto dto, CancellationToken cancellationToken)
    {
        var existing = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        existing.Status = "Cancelled";
        existing.Concellation = dto.CancellationReason;
        await _appointmentRepository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<string?> GetStatusAsync(Guid id, CancellationToken cancellationToken)
    {
        var appointment = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        return appointment?.Status;
    }

    public async Task<bool> UpdateStatusAsync(Guid id, UpdateAppointmentStatusDto dto, CancellationToken cancellationToken)
    {
        var existing = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        existing.Status = dto.Status;
        await _appointmentRepository.UpdateAsync(existing, cancellationToken);
        return true;
    }

    public async Task<bool> UpdateDateTimeAsync(Guid id, UpdateAppointmentDateTimeDto dto, CancellationToken cancellationToken)
    {
        var existing = await _appointmentRepository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        existing.DateTime = dto.ScheduledAt;
        await _appointmentRepository.UpdateAsync(existing, cancellationToken);
        return true;
    }
}
