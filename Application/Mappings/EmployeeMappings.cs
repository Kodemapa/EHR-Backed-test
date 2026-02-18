using EHR_Application.Domain.Models;
using EHR_Application.Application.Dtos;

namespace EHR_Application.Application.Mappings;

public static class EmployeeMappings
{
    /// <summary>
    /// Converts an Employee entity to EmployeeDto
    /// </summary>
    public static EmployeeDto ToDto(this Employee employee)
    {
        return new EmployeeDto
        {
            Id = employee.Id,
            EmployeeId = employee.EmployeeId,
            FirstName = employee.FirstName,
            LastName = employee.LastName,
            Gender = employee.Gender, // Direct mapping
            DateOfBirth = employee.DateOfBirth,
            PhoneNumber = employee.PhoneNumber,
            EmailAddress = employee.EmailAddress,
            Address = employee.Address,
            Department = employee.Department,
            Designation = employee.Designation,
            JoiningDate = employee.JoiningDate,
            EmploymentType = employee.EmploymentType, // Direct mapping
            BasicPay = employee.BasicPay,
            Allowances = employee.Allowances,
            Deductions = employee.Deductions,
            PfTaxInfo = employee.PfTaxInfo,
            Status = employee.Status,
            OfferLetter = employee.OfferLetter,
            Resume = employee.Resume,
            IdProof = employee.IdProof
        };
    }

    /// <summary>
    /// Converts CreateEmployeeDto to Employee entity
    /// </summary>
    public static Employee ToEntity(this CreateEmployeeDto dto)
    {
        return new Employee
        {
            Id = Guid.NewGuid(),
            // EmployeeId will be set by the service
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender, // Direct mapping
            DateOfBirth = dto.DateOfBirth,
            PhoneNumber = dto.PhoneNumber,
            EmailAddress = dto.EmailAddress,
            Address = dto.Address,
            Department = dto.Department,
            Designation = dto.Designation,
            JoiningDate = dto.JoiningDate,
            EmploymentType = dto.EmploymentType, // Direct mapping
            BasicPay = dto.BasicPay,
            Allowances = dto.Allowances,
            Deductions = dto.Deductions,
            PfTaxInfo = dto.PfTaxInfo,
            Status = "Active",
            OfferLetter = dto.OfferLetter,
            Resume = dto.Resume,
            IdProof = dto.IdProof
        };
    }

    /// <summary>
    /// Updates an existing Employee entity with values from UpdateEmployeeDto
    /// </summary>
    public static void UpdateFromDto(this Employee employee, UpdateEmployeeDto dto)
    {
        if (!string.IsNullOrEmpty(dto.FirstName))
            employee.FirstName = dto.FirstName;

        if (!string.IsNullOrEmpty(dto.LastName))
            employee.LastName = dto.LastName;

        if (!string.IsNullOrEmpty(dto.Gender))
            employee.Gender = dto.Gender; // Direct mapping

        if (dto.DateOfBirth.HasValue)
            employee.DateOfBirth = dto.DateOfBirth.Value;

        if (!string.IsNullOrEmpty(dto.PhoneNumber))
            employee.PhoneNumber = dto.PhoneNumber;

        if (!string.IsNullOrEmpty(dto.EmailAddress))
            employee.EmailAddress = dto.EmailAddress;

        if (dto.Address != null)
            employee.Address = dto.Address;

        if (!string.IsNullOrEmpty(dto.Department))
            employee.Department = dto.Department;

        if (!string.IsNullOrEmpty(dto.Designation))
            employee.Designation = dto.Designation;

        if (dto.JoiningDate.HasValue)
            employee.JoiningDate = dto.JoiningDate.Value;

        if (!string.IsNullOrEmpty(dto.EmploymentType))
            employee.EmploymentType = dto.EmploymentType; // Direct mapping

        if (dto.BasicPay.HasValue)
            employee.BasicPay = dto.BasicPay.Value;

        if (dto.Allowances.HasValue)
            employee.Allowances = dto.Allowances;

        if (dto.Deductions.HasValue)
            employee.Deductions = dto.Deductions;

        if (dto.PfTaxInfo != null)
            employee.PfTaxInfo = dto.PfTaxInfo;

        if (!string.IsNullOrEmpty(dto.Status))
            employee.Status = dto.Status;
            
        if (dto.OfferLetter != null)
            employee.OfferLetter = dto.OfferLetter;
            
        if (dto.Resume != null)
            employee.Resume = dto.Resume;
            
        if (dto.IdProof != null)
            employee.IdProof = dto.IdProof;
    }

    /// <summary>
    /// Converts a collection of Employee entities to EmployeeDtos
    /// </summary>
    public static IEnumerable<EmployeeDto> ToDtoList(this IEnumerable<Employee> employees)
    {
        return employees.Select(e => e.ToDto());
    }
}
