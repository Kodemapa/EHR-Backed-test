namespace EHR_Application.Application.Dtos;

public sealed class CreatePrescriptionDto
{
    public string PatientId { get; set; } = string.Empty; // Code/MRN
    public string DoctorId { get; set; } = string.Empty; // Registration/Code
    public string Diagnosis { get; set; } = string.Empty;
    public int IssueDate { get; set; }
    public List<PrescriptionMedicationDto> Medications { get; set; } = new();
    public string? AdditionalInstructions { get; set; }
}
