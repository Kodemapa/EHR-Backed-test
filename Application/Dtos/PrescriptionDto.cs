namespace EHR_Application.Application.Dtos;

public sealed class PrescriptionDto
{
    public Guid Id { get; set; } // Internal ID
    public string PrescriptionId { get; set; } = string.Empty; // Display ID e.g. RX-2026-001
    public string PatientName { get; set; } = string.Empty;
    public string DoctorName { get; set; } = string.Empty;
    public int IssueDate { get; set; }
    public int MedicationCount { get; set; }
    public string Status { get; set; } = string.Empty;
}
