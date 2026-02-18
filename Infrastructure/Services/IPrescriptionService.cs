using EHR_Application.Application.Dtos;

namespace EHR_Application.Infrastructure.Services;

public interface IPrescriptionService
{
    Task<IEnumerable<PrescriptionDto>> GetAllAsync(CancellationToken cancellationToken);
    Task<PrescriptionViewDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<PrescriptionDto> CreateAsync(CreatePrescriptionDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, UpdatePrescriptionDto dto, CancellationToken cancellationToken);
    Task<bool> CancelAsync(Guid id, CancelPrescriptionDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<byte[]> PrintAsync(Guid id, CancellationToken cancellationToken);
}
