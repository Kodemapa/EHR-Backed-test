using EHR_Application.Application.Dtos;

namespace EHR_Application.Infrastructure.Services;

public interface IInsuranceService
{
    Task<IEnumerable<InsuranceClaimDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<InsuranceClaimDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<InsuranceClaimDto> CreateAsync(CreateInsuranceClaimDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateStatusAsync(Guid id, UpdateInsuranceClaimStatusDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken);
}
