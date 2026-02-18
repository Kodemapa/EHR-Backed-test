namespace EHR_Application.Domain.Models;

public class Doctor
{
    public Guid Id { get; set; }

    // Basic Information
    public string? Title { get; set; }  // e.g., Dr., Prof., etc.
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    // Professional Information
    public string? Specialization { get; set; }
    public string? Degree { get; set; }
    public string? Qualification { get; set; }
    public int? ExperienceYears { get; set; }
    public string? Registration { get; set; }  // Medical registration number
    public int? PF { get; set; }  // Provident Fund number

    // Contact Information
    public string? Email { get; set; }
    public long? PhoneNumber { get; set; }
    public string? PhoneResidence { get; set; }
    public long? OfficeNumber { get; set; }

    // Location Information
    public string? NameOfCentre { get; set; }
    public string? Address { get; set; }
    public string? Area { get; set; }
    public string? City { get; set; }
    public int? Pincode { get; set; }

    // Document Storage
    public string? Document { get; set; }  // Stores PDF file path or base64 string

    // Status
    public bool IsActive { get; set; } = true;
}