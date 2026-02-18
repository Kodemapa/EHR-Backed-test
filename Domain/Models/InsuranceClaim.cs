using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_Application.Domain.Models;

public class InsuranceClaim
{
    [Key]
    public Guid Id { get; set; }

   
    public string ClaimId { get; set; } = string.Empty; // e.g., #CLM-5001

    
    public Guid PatientId { get; set; }

    [ForeignKey("PatientId")]
    public Patient Patient { get; set; } = null!;

    public string PatientName { get; set; } = string.Empty;


    public string ProviderName { get; set; } = string.Empty;

 
    public int PolicyNumber { get; set; }

   
    public string TreatmentType { get; set; } = string.Empty;

    public string NetworkHospital { get; set; } = string.Empty;

    [Column(TypeName = "decimal(18,2)")]
    public decimal ClaimedAmount { get; set; }

    [Column(TypeName = "decimal(18,2)")]
    public decimal ApprovedAmount { get; set; }

    public int CoveragePercentage { get; set; }

   
    public string Status { get; set; } = "Pending"; // Pending, Approved, Rejected

    public string Remarks { get; set; } = string.Empty;

    public DateTime SubmissionDate { get; set; } = DateTime.UtcNow;

    public DateTime? ExpiryDate { get; set; }

    public DateTime? ApprovalDate { get; set; }
}
