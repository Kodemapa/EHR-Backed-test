using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_Application.Domain.Models;

public sealed class PrescriptionMedication
{
    public Guid Id { get; set; }
    
    [Column("Prescription Id")]
    public Guid PrescriptionId { get; set; }
    
    [Column("Medicine Name")]
    public string MedicineName { get; set; } = string.Empty;
    
    [Column("Dosage")]
    public string Dosage { get; set; } = string.Empty;
    
    [Column("Frequency")]
    public string Frequency { get; set; } = string.Empty;
    
    [Column("Duration")]
    public string Duration { get; set; } = string.Empty;
    
    [Column("Notes")]
    public string? Notes { get; set; }

    // Navigation property
    public Prescription? Prescription { get; set; }
}
