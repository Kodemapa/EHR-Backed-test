using EHR_Application.Domain.Models;

namespace EHR_Application.Infrastructure.Repositories;

public interface IPrescriptionRepository
{
    Task<IEnumerable<Prescription>> GetAllAsync(CancellationToken cancellationToken);
    Task<Prescription?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<Prescription> AddAsync(Prescription prescription, CancellationToken cancellationToken);
    Task UpdateAsync(Guid id, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<Prescription?> GetByCodeAsync(string code, CancellationToken cancellationToken);
    Task<string> GetLatestCodeAsync(CancellationToken cancellationToken);
}
