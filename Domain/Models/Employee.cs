using System.ComponentModel.DataAnnotations;

namespace EHR_Application.Domain.Models;

public class Employee
{
    [Key]
    public Guid Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string EmployeeId { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [MaxLength(20)] // Allow any gender string
    public string Gender { get; set; } = string.Empty;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required]
    [MaxLength(20)]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [MaxLength(255)]
    public string EmailAddress { get; set; } = string.Empty;

    public string? Address { get; set; }

    [Required]
    [MaxLength(100)]
    public string Department { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string Designation { get; set; } = string.Empty;

    [Required]
    public DateOnly JoiningDate { get; set; }

    [Required]
    [MaxLength(50)] // Allow any employment type string
    public string EmploymentType { get; set; } = string.Empty;

    [Required]
    public decimal BasicPay { get; set; }

    public decimal? Allowances { get; set; }

    public decimal? Deductions { get; set; }

    public string? PfTaxInfo { get; set; }

    [Required]
    [MaxLength(50)]
    public string Status { get; set; } = "Active";

    public string? OfferLetter { get; set; }
    
    public string? Resume { get; set; }
    public string? IdProof { get; set; }
}
