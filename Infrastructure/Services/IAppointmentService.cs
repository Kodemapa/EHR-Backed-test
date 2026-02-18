using EHR_Application.Application.Dtos;

namespace EHR_Application.Infrastructure.Services;

public interface IAppointmentService
{
    // CRUD
    Task<IEnumerable<AppointmentViewDto>> GetAsync(CancellationToken cancellationToken);
    Task<AppointmentViewDto?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<AppointmentViewDto>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken);
    Task<AppointmentDto> CreateAsync(CreateAppointmentDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, UpdateAppointmentDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    // View (NEW)
    Task<AppointmentViewDto?> GetViewAsync(
        Guid id,
        CancellationToken cancellationToken);

    // New Endpoints
    Task<bool> RescheduleAsync(Guid id, RescheduleAppointmentDto dto, CancellationToken cancellationToken);
    Task<bool> CancelAsync(Guid id, CancelAppointmentDto dto, CancellationToken cancellationToken);
    Task<string?> GetStatusAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UpdateStatusAsync(Guid id, UpdateAppointmentStatusDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateDateTimeAsync(Guid id, UpdateAppointmentDateTimeDto dto, CancellationToken cancellationToken);
}
