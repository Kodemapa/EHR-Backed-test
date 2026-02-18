using System.ComponentModel.DataAnnotations;

namespace EHR_Application.Application.Dtos;

public class UpdateEmployeeDto
{
    [MaxLength(100)]
    public string? FirstName { get; set; }

    [MaxLength(100)]
    public string? LastName { get; set; }

    public string? Gender { get; set; }

    public DateOnly? DateOfBirth { get; set; }

    [MaxLength(20)]
    public string? PhoneNumber { get; set; }

    [EmailAddress]
    [MaxLength(255)]
    public string? EmailAddress { get; set; }

    public string? Address { get; set; }

    [MaxLength(100)]
    public string? Department { get; set; }

    [MaxLength(100)]
    public string? Designation { get; set; }

    public DateOnly? JoiningDate { get; set; }

    public string? EmploymentType { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? BasicPay { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Allowances { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Deductions { get; set; }

    public string? PfTaxInfo { get; set; }

    public string? Status { get; set; }

    public string? OfferLetter { get; set; }
    
    // New fields
    public string? Resume { get; set; }
    public string? IdProof { get; set; }
}
