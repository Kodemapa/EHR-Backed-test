using System.ComponentModel.DataAnnotations;

namespace EHR_Application.Application.Dtos;

public class CreateInsuranceClaimDto
{
    [Required]
    public Guid PatientId { get; set; }
    
   
    public string ProviderName { get; set; } = string.Empty;
    
  
    public int PolicyNumber { get; set; }
    
    
    public string TreatmentType { get; set; } = string.Empty;
    

    public string NetworkHospital { get; set; } = string.Empty;
    
 
    [Range(0, double.MaxValue, ErrorMessage = "Claimed amount must be a non-negative value.")]
    public decimal ClaimedAmount { get; set; }

    public DateTime? ExpiryDate { get; set; }
    
    public string? Remarks { get; set; }
}
