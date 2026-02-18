using EHR_Application.Application.Dtos;
using EHR_Application.Application.Mappings;
using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace EHR_Application.Infrastructure.Services;

public class InsuranceService : IInsuranceService
{
    private readonly IInsuranceRepository _repository;
    private readonly IPatientRepository _patientRepository;
    private readonly ILogger<InsuranceService> _logger;

    public InsuranceService(IInsuranceRepository repository, IPatientRepository patientRepository, ILogger<InsuranceService> logger)
    {
        _repository = repository;
        _patientRepository = patientRepository;
        _logger = logger;
    }

    public async Task<IEnumerable<InsuranceClaimDto>> GetAllAsync(CancellationToken cancellationToken)
    {
        var claims = await _repository.GetAllAsync(cancellationToken);
        return claims.Select(c => c.ToDto());
    }

    public async Task<InsuranceClaimDto?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var claim = await _repository.GetByIdAsync(id, cancellationToken);
        return claim?.ToDto();
    }

    public async Task<InsuranceClaimDto> CreateAsync(CreateInsuranceClaimDto dto, CancellationToken cancellationToken)
    {
        // 🔹 1. Validate that the patient exists to avoid Foreign Key violation
        var patient = await _patientRepository.GetByIdAsync(dto.PatientId, cancellationToken);
        if (patient == null)
        {
            throw new KeyNotFoundException($"Patient with ID {dto.PatientId} not found.");
        }

        var entity = dto.ToEntity();
        
        // 🔹 2. Ensure the patient name is captured in the claim
        entity.PatientName = $"{patient.FirstName} {patient.LastName}";

        // Generate a claim ID (e.g. #CLM-xxxx)
        var random = new Random();
        entity.ClaimId = $"#CLM-{random.Next(1000, 9999)}";
        entity.Id = Guid.NewGuid();

        var created = await _repository.AddAsync(entity, cancellationToken);
        _logger.LogInformation("Created new insurance claim {Id}", created.Id);
        
        return created.ToDto();
    }

    public async Task<bool> UpdateStatusAsync(Guid id, UpdateInsuranceClaimStatusDto dto, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        existing.UpdateFromDto(dto);
        await _repository.UpdateAsync(existing, cancellationToken);
        _logger.LogInformation("Updated claim {Id} status to {Status}", id, dto.Status);
        return true;
    }

    public async Task<bool> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _repository.GetByIdAsync(id, cancellationToken);
        if (existing is null) return false;

        await _repository.DeleteAsync(id, cancellationToken);
        _logger.LogInformation("Deleted claim {Id}", id);
        return true;
    }

    public async Task<DashboardStatsDto> GetDashboardStatsAsync(CancellationToken cancellationToken)
    {
        var activePolicies = await _repository.CountActivePoliciesAsync(cancellationToken);
        var pending = await _repository.CountByStatusAsync("Pending", cancellationToken);
        var approved = await _repository.CountByStatusAsync("Approved", cancellationToken);
        var rejected = await _repository.CountByStatusAsync("Rejected", cancellationToken);
        var totalApproved = await _repository.SumApprovedAmountAsync(cancellationToken);
        
        return new DashboardStatsDto
        {
            TotalClaims = pending + approved + rejected,
            ActivePoliciesCount = activePolicies,
            PendingClaimsCount = pending,
            ApprovedClaimsCount = approved,
            RejectedClaimsCount = rejected,
            TotalApprovedAmount = totalApproved
        };
    }
}
