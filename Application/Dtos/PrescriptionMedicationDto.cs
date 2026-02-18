namespace EHR_Application.Application.Dtos;

public sealed class PrescriptionMedicationDto
{
    public string MedicineName { get; set; } = string.Empty;
    public string Dosage { get; set; } = string.Empty;
    public string Frequency { get; set; } = string.Empty;
    public string Duration { get; set; } = string.Empty;
    public string? Notes { get; set; }
}
