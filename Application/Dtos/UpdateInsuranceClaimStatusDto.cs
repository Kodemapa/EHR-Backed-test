using System.ComponentModel.DataAnnotations;

namespace EHR_Application.Application.Dtos;

public class UpdateInsuranceClaimStatusDto
{
    [Required]
    public string Status { get; set; } = string.Empty;
    
    [Required]
    public decimal ApprovedAmount { get; set; }
    
    public int? CoveragePercentage { get; set; }
    
    public string? PatientName { get; set; }
    
    public string? Remarks { get; set; }
}
