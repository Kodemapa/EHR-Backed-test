using EHR_Application.Application.Dtos;
using EHR_Application.Domain.Models;

namespace EHR_Application.Application.Mappings;

public static class InsuranceMappings
{
    public static InsuranceClaimDto ToDto(this InsuranceClaim entity)
    {
        return new InsuranceClaimDto
        {
            Id = entity.Id,
            ClaimId = entity.ClaimId,
            PatientId = entity.PatientId,
            PatientName = !string.IsNullOrEmpty(entity.PatientName) ? entity.PatientName : (entity.Patient != null ? $"{entity.Patient.FirstName} {entity.Patient.LastName}" : string.Empty),
            ProviderName = entity.ProviderName,
            PolicyNumber = entity.PolicyNumber,
            TreatmentType = entity.TreatmentType,
            NetworkHospital = entity.NetworkHospital,
            ClaimedAmount = entity.ClaimedAmount,
            ApprovedAmount = entity.ApprovedAmount,
            CoveragePercentage = entity.CoveragePercentage,
            Status = entity.Status,
            Remarks = entity.Remarks,
            SubmissionDate = entity.SubmissionDate,
            ExpiryDate = entity.ExpiryDate,
            ApprovalDate = entity.ApprovalDate
        };
    }

    public static InsuranceClaim ToEntity(this CreateInsuranceClaimDto dto)
    {
        return new InsuranceClaim
        {
            PatientId = dto.PatientId,
            ProviderName = dto.ProviderName,
            PolicyNumber = dto.PolicyNumber,
            TreatmentType = dto.TreatmentType,
            NetworkHospital = dto.NetworkHospital,
            ClaimedAmount = dto.ClaimedAmount,
            Status = "Pending",
            ExpiryDate = dto.ExpiryDate,
            Remarks = dto.Remarks ?? string.Empty,
            SubmissionDate = DateTime.UtcNow
        };
    }

    public static void UpdateFromDto(this InsuranceClaim entity, UpdateInsuranceClaimStatusDto dto)
    {
        entity.Status = dto.Status;
        entity.ApprovedAmount = dto.ApprovedAmount;
        if (dto.CoveragePercentage.HasValue)
        {
            entity.CoveragePercentage = dto.CoveragePercentage.Value;
        }
        if (!string.IsNullOrEmpty(dto.PatientName))
        {
            entity.PatientName = dto.PatientName;
        }
        if (!string.IsNullOrEmpty(dto.Remarks))
        {
            entity.Remarks = dto.Remarks;
        }
        
        if (dto.Status == "Approved" || dto.Status == "Rejected")
        {
            entity.ApprovalDate = DateTime.UtcNow;
        }
    }
}
