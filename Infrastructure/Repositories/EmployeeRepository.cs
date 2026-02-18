using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EHR_Application.Infrastructure.Repositories;

public class EmployeeRepository : IEmployeeRepository
{
    private readonly EhrDbContext _context;

    public EmployeeRepository(EhrDbContext context)
    {
        _context = context;
    }

    public async Task<Employee?> GetByIdAsync(Guid id)
    {
        return await _context.Employees.FindAsync(id);
    }

    public async Task<IEnumerable<Employee>> GetAllAsync()
    {
        return await _context.Employees
            .OrderBy(e => e.EmployeeId)
            .ToListAsync();
    }

    public async Task<string> GenerateNextEmployeeIdAsync()
    {
        // Use a DB sequence to ensure concurrency safety and avoid race conditions.
        // This requires the "EmployeeIdSequence" to be created via migration.
        var result = await _context.Database.SqlQueryRaw<int>("SELECT nextval('\"EmployeeIdSequence\"')").ToListAsync();
        var nextId = result.FirstOrDefault();

        return $"EMP-{nextId:D3}";
    }

    public async Task<Employee> CreateAsync(Employee employee)
    {
        employee.Id = Guid.NewGuid();

        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<Employee> UpdateAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();

        return employee;
    }

    public async Task<bool> DeleteAsync(Guid id)
    {
        var employee = await GetByIdAsync(id);
        if (employee == null)
        {
            return false;
        }

        // Hard delete
        _context.Employees.Remove(employee);

        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> EmailExistsAsync(string email, Guid? excludeId = null)
    {
        var query = _context.Employees.Where(e => e.EmailAddress == email);

        if (excludeId.HasValue)
        {
            query = query.Where(e => e.Id != excludeId.Value);
        }

        return await query.AnyAsync();
    }


}
