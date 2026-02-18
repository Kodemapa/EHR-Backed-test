namespace EHR_Application.Application.Dtos;

public sealed class UpdatePrescriptionDto
{
    public string Diagnosis { get; set; } = string.Empty;
    public List<PrescriptionMedicationDto> Medications { get; set; } = new();
}
