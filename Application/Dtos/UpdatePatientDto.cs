using System.ComponentModel.DataAnnotations;

namespace EHR_Application.Application.Dtos;

public sealed class UpdatePatientDto
{
    [Required]
    public string FirstName { get; set; } = string.Empty;
    
    [Required]
    public string LastName { get; set; } = string.Empty;
    
    [Required]
    public DateOnly DateOfBirth { get; set; }
    
    [Required]
    public string MedicalRecordNumber { get; set; } = string.Empty;
    
    // Additional patient information
    public string IdProof { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;

    [Required]
    public long PhoneNumber { get; set; }

    [Required]
    [EmailAddress(ErrorMessage = "Invalid Email Address.")]
    public string Email { get; set; } = string.Empty;

    public string AddressLine { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;

    [Required]
    public int Pincode { get; set; }

    public string Country { get; set; } = string.Empty;
    public string MedicalInfo { get; set; } = string.Empty;
    public string Vaccines { get; set; } = string.Empty;
    public string Documents { get; set; } = "[]"; // JSON array of file paths

    public int Age { get; set; }
    public DateOnly? LastVisit { get; set; }
    public string Status { get; set; } = string.Empty;
}
