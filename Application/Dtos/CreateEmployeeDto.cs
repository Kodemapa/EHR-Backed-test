using System.ComponentModel.DataAnnotations;

namespace EHR_Application.Application.Dtos;

public class CreateEmployeeDto
{
    [Required]
    [MaxLength(100)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(100)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    public string Gender { get; set; } = string.Empty;

    [Required]
    public DateOnly DateOfBirth { get; set; }

    [Required(ErrorMessage = "Phone Number is required.")]
    [RegularExpression(@"^\+?\d{10,15}$", ErrorMessage = "Phone number must be between 10 and 15 digits.")]
    public string PhoneNumber { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
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
    public string EmploymentType { get; set; } = string.Empty;

    [Required]
    [Range(0, double.MaxValue)]
    public decimal BasicPay { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Allowances { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Deductions { get; set; }

    public string? PfTaxInfo { get; set; }

    public string? OfferLetter { get; set; }
    
    // New fields
    public string? Resume { get; set; }
    public string? IdProof { get; set; }
}
