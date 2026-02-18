using EHR_Application.Application.Dtos;

namespace EHR_Application.Infrastructure.Services;

public interface IDoctorService
{
    Task<IEnumerable<DoctorDto>> GetAsync(CancellationToken cancellationToken);

    Task<DoctorDto?> GetAsync(Guid id, CancellationToken cancellationToken);

    Task<DoctorDto> CreateAsync(CreateDoctorDto dto, CancellationToken cancellationToken);

    Task<bool> UpdateAsync(Guid id, UpdateDoctorDto dto, CancellationToken cancellationToken);

    Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken);

    Task<IEnumerable<DoctorDto>> GetBySpecializationAsync(string specialization, CancellationToken cancellationToken);

    Task<bool> ToggleStatusAsync(Guid id, CancellationToken cancellationToken);

    Task<bool> UploadDocumentAsync(Guid id, Microsoft.AspNetCore.Http.IFormFile file, CancellationToken cancellationToken);
}