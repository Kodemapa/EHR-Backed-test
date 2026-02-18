using EHR_Application.Application.Dtos;

namespace EHR_Application.Infrastructure.Services;

public interface IEmployeeService
{
    Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync();
    Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id);

    Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto);
    Task<EmployeeDto?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateDto);
    Task<EmployeeDto?> UpdateCompensationAsync(Guid id, UpdateEmployeeCompensationDto compensationDto);
    Task<bool> DeleteEmployeeAsync(Guid id);
    Task<bool> UploadDocumentAsync(Guid id, string documentType, Microsoft.AspNetCore.Http.IFormFile file);
}
