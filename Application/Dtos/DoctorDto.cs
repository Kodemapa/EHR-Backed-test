namespace EHR_Application.Application.Dtos;

public sealed class DoctorDto
{
    public Guid Id { get; set; }

    // Basic Information
    public string? Title { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Gender { get; set; }
    public DateOnly? DateOfBirth { get; set; }

    // Professional Information
    public string? Specialization { get; set; }
    public string? Degree { get; set; }
    public string? Qualification { get; set; }
    public int? ExperienceYears { get; set; }
    public string? Registration { get; set; }
    public int? PF { get; set; }

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

    // Document
    public string? Document { get; set; }

    // Status
    public bool IsActive { get; set; }
}