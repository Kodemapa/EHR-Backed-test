using EHR_Application.Domain.Models;

namespace EHR_Application.Infrastructure.Repositories;

public interface IAppointmentRepository
{
    Task<IEnumerable<Appointment>> GetAllAsync(CancellationToken cancellationToken);
    Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken);
    Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
}
