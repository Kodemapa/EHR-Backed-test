using EHR_Application.Domain.Models;

namespace EHR_Application.Infrastructure.Repositories;

public interface IInsuranceRepository
{
    Task<IEnumerable<InsuranceClaim>> GetAllAsync(CancellationToken cancellationToken);
    Task<InsuranceClaim?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<InsuranceClaim> AddAsync(InsuranceClaim claim, CancellationToken cancellationToken);
    Task UpdateAsync(InsuranceClaim claim, CancellationToken cancellationToken);
    Task DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<InsuranceClaim>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken);
    Task<int> CountByStatusAsync(string status, CancellationToken cancellationToken);
    Task<decimal> SumApprovedAmountAsync(CancellationToken cancellationToken);
    Task<int> CountActivePoliciesAsync(CancellationToken cancellationToken);
}
