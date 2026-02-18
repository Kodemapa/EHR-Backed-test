using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_Application.Domain.Models;

public sealed class Prescription
{
    public Guid Id { get; set; }
    
    [Column("Prescription Code")]
    public string PrescriptionCode { get; set; } = string.Empty; // e.g., RX-2026-001
    
    [Column("Patient Id")]
    public Guid PatientId { get; set; }
    
    [Column("Doctor Id")]
    public Guid DoctorId { get; set; }
    
    [Column("Diagnosis")]
    public string Diagnosis { get; set; } = string.Empty;
    
    [Column("Issue Date")]
    public int IssueDate { get; set; }
    
    [Column("Status")]
    public string Status { get; set; } = "ACTIVE";
    
    [Column("Additional Instructions")]
    public string? AdditionalInstructions { get; set; } 

    [Column("Cancellation Reason")]
    public string? CancellationReason { get; set; }

    // Navigation properties
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
    public ICollection<PrescriptionMedication> Medications { get; set; } = new List<PrescriptionMedication>();
}
