namespace EHR_Application.Application.Dtos;

public sealed class PatientDto
{
    public Guid Id { get; set; }
    public string FullName { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public string MedicalRecordNumber { get; set; } = string.Empty;

     // New fields
    public string IdProof { get; set; } = string.Empty;
    public string Gender { get; set; } = string.Empty;
    public long PhoneNumber { get; set; }
    public string Email { get; set; } = string.Empty;
    public string AddressLine { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string State { get; set; } = string.Empty;
    public int Pincode { get; set; }
    public string Country { get; set; } = string.Empty;
    public string MedicalInfo { get; set; } = string.Empty;
    public string Vaccines { get; set; } = string.Empty;
    public string Documents { get; set; } = "[]"; // JSON array of file paths

    public int Age { get; set; }
    public DateOnly? LastVisit { get; set; }
    public string Status { get; set; } = string.Empty;
}
