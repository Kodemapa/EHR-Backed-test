using EHR_Application.Application.Dtos;
using EHR_Application.Application.Mappings;
using EHR_Application.Infrastructure.Repositories;
using Microsoft.AspNetCore.Http;
using System.IO;

namespace EHR_Application.Infrastructure.Services;

public class EmployeeService : IEmployeeService
{
    private readonly IEmployeeRepository _repository;

    public EmployeeService(IEmployeeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IEnumerable<EmployeeDto>> GetAllEmployeesAsync()
    {
        var employees = await _repository.GetAllAsync();
        return employees.ToDtoList();
    }

    public async Task<EmployeeDto?> GetEmployeeByIdAsync(Guid id)
    {
        var employee = await _repository.GetByIdAsync(id);
        return employee?.ToDto();
    }


    public async Task<EmployeeDto> CreateEmployeeAsync(CreateEmployeeDto createDto)
    {
        // Check if email already exists
        if (await _repository.EmailExistsAsync(createDto.EmailAddress))
        {
            throw new InvalidOperationException($"Email '{createDto.EmailAddress}' is already in use.");
        }

        var employee = createDto.ToEntity();
        
        // Generate next EmployeeId
        employee.EmployeeId = await _repository.GenerateNextEmployeeIdAsync();

        var createdEmployee = await _repository.CreateAsync(employee);
        return createdEmployee.ToDto();
    }

    public async Task<EmployeeDto?> UpdateEmployeeAsync(Guid id, UpdateEmployeeDto updateDto)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
            return null;
        }

        // Check email uniqueness if email is being updated
        if (!string.IsNullOrEmpty(updateDto.EmailAddress) && 
            updateDto.EmailAddress != employee.EmailAddress)
        {
            if (await _repository.EmailExistsAsync(updateDto.EmailAddress, id))
            {
                throw new InvalidOperationException($"Email '{updateDto.EmailAddress}' is already in use.");
            }
        }

        employee.UpdateFromDto(updateDto);
        
        var updatedEmployee = await _repository.UpdateAsync(employee);
        return updatedEmployee.ToDto();
    }

    public async Task<EmployeeDto?> UpdateCompensationAsync(Guid id, UpdateEmployeeCompensationDto compensationDto)
    {
        var employee = await _repository.GetByIdAsync(id);
        if (employee == null)
        {
            return null;
        }

        employee.BasicPay = compensationDto.BasicPay;
        employee.Allowances = compensationDto.Allowances;
        employee.Deductions = compensationDto.Deductions;
        employee.PfTaxInfo = compensationDto.PfTaxInfo;

        var updatedEmployee = await _repository.UpdateAsync(employee);
        return updatedEmployee.ToDto();
    }

    public async Task<bool> UploadDocumentAsync(Guid id, string documentType, IFormFile file)
    {
        const long MAX_UPLOAD_BYTES = 5 * 1024 * 1024; // 5 MB

        var employee = await _repository.GetByIdAsync(id);
        if (employee == null) return false;
        
        if (file == null || file.Length == 0) return false;
        if (file.Length > MAX_UPLOAD_BYTES) return false;

        var allowedExtensions = new[] { ".pdf", ".jpg", ".jpeg", ".png", ".docx", ".doc" };
        var extension = Path.GetExtension(file.FileName).ToLowerInvariant();
        if (!allowedExtensions.Contains(extension)) return false;

        var folderName = Path.Combine("wwwroot", "uploads", "employees");
        var pathToSave = Path.Combine(Directory.GetCurrentDirectory(), folderName);
        if (!Directory.Exists(pathToSave)) Directory.CreateDirectory(pathToSave);

        // Determine existing file path based on document type
        string? existingDbPath = null;
        switch (documentType.ToLower())
        {
            case "offer-letter":
                existingDbPath = employee.OfferLetter;
                break;
            case "resume":
                existingDbPath = employee.Resume;
                break;
            case "id-proof":
                existingDbPath = employee.IdProof;
                break;
            default:
                return false;
        }

        // Delete existing file if it exists
        if (!string.IsNullOrEmpty(existingDbPath))
        {
            try
            {
                var existingFileName = Path.GetFileName(existingDbPath);
                var existingFullPath = Path.Combine(pathToSave, existingFileName);
                if (File.Exists(existingFullPath))
                {
                    File.Delete(existingFullPath);
                }
            }
            catch
            {
                // Safely ignore deletion errors
            }
        }

        var fileName = $"{id}_{documentType}_{Guid.NewGuid()}{extension}";
        var fullPath = Path.Combine(pathToSave, fileName);
        var dbPath = Path.Combine("uploads", "employees", fileName).Replace("\\", "/");

        using (var stream = new FileStream(fullPath, FileMode.Create))
        {
            await file.CopyToAsync(stream);
        }

        switch (documentType.ToLower())
        {
            case "offer-letter":
                employee.OfferLetter = dbPath;
                break;
            case "resume":
                employee.Resume = dbPath;
                break;
            case "id-proof":
                employee.IdProof = dbPath;
                break;
        }

        await _repository.UpdateAsync(employee);
        return true;
    }

    public async Task<bool> DeleteEmployeeAsync(Guid id)
    {
        return await _repository.DeleteAsync(id);
    }

}
