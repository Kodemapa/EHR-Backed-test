namespace EHR_Application.Application.Dtos;

public sealed class PrescriptionViewDto
{
    public string PrescriptionId { get; set; } = string.Empty;
    public PrescriptionPatientDto Patient { get; set; } = new();
    public PrescriptionDoctorDto Doctor { get; set; } = new();
    public string Diagnosis { get; set; } = string.Empty;
    public int IssueDate { get; set; }
    public string Status { get; set; } = string.Empty;
    public List<PrescriptionMedicationDto> Medications { get; set; } = new();
    public string? AdditionalInstructions { get; set; }
}

public sealed class PrescriptionPatientDto
{
    public string Name { get; set; } = string.Empty;
    public int Age { get; set; }
    public string Gender { get; set; } = string.Empty;
}

public sealed class PrescriptionDoctorDto
{
    public string Name { get; set; } = string.Empty;
    public string? Specialization { get; set; }
}
