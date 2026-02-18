using EHR_Application.Domain.Models;

namespace EHR_Application.Infrastructure.Repositories;

public interface IPatientRepository
{
    Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken);
    Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken);
    Task UpdateAsync(Patient patient, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Patient?> GetByMRNAsync(string mrn, CancellationToken cancellationToken);
}
