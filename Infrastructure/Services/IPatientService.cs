using EHR_Application.Application.Dtos;

namespace EHR_Application.Infrastructure.Services;

public interface IPatientService
{
    Task<IEnumerable<PatientDto>> GetAsync(CancellationToken cancellationToken);
    Task<PatientDto?> GetAsync(Guid id, CancellationToken cancellationToken);
    Task<PatientDto> CreateAsync(CreatePatientDto dto, CancellationToken cancellationToken);
    Task<bool> UpdateAsync(Guid id, UpdatePatientDto dto, CancellationToken cancellationToken);
    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> UploadDocumentAsync(Guid id, Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken);
}
