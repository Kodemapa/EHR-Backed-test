using System;

namespace EHR_Application.Application.Dtos;

public class InsuranceClaimDto
{
    public Guid Id { get; set; }
    public string ClaimId { get; set; } = string.Empty;
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public string ProviderName { get; set; } = string.Empty;
    public int PolicyNumber { get; set; }
    public string TreatmentType { get; set; } = string.Empty;
    public string NetworkHospital { get; set; } = string.Empty;
    public decimal ClaimedAmount { get; set; }
    public decimal ApprovedAmount { get; set; }
    public int CoveragePercentage { get; set; }
    public string Status { get; set; } = string.Empty;
    public string Remarks { get; set; } = string.Empty;
    public DateTime SubmissionDate { get; set; }
    public DateTime? ExpiryDate { get; set; }
    public DateTime? ApprovalDate { get; set; }
}
