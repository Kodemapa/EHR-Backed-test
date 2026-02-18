using EHR_Application.Domain.Models;

namespace EHR_Application.Infrastructure.Repositories;

public interface IEmployeeRepository
{
    Task<Employee?> GetByIdAsync(Guid id);
    Task<IEnumerable<Employee>> GetAllAsync();
    Task<string> GenerateNextEmployeeIdAsync();
    Task<Employee> CreateAsync(Employee employee);
    Task<Employee> UpdateAsync(Employee employee);
    Task<bool> DeleteAsync(Guid id);
    Task<bool> EmailExistsAsync(string email, Guid? excludeId = null);
}
