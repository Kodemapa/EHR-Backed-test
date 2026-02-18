namespace EHR_Application.Application.Dtos;

public class DashboardStatsDto
{
    public int TotalClaims { get; set; }
    public int ActivePoliciesCount { get; set; }
    public int PendingClaimsCount { get; set; }
    public int ApprovedClaimsCount { get; set; }
    public int RejectedClaimsCount { get; set; }
    public decimal TotalApprovedAmount { get; set; }
}
