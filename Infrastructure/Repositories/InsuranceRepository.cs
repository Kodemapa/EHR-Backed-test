using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EHR_Application.Infrastructure.Repositories;

public class InsuranceRepository : IInsuranceRepository
{
    private readonly EhrDbContext _context;

    public InsuranceRepository(EhrDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<InsuranceClaim>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.InsuranceClaims
            .Include(c => c.Patient)
            .OrderByDescending(c => c.SubmissionDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<InsuranceClaim?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.InsuranceClaims
            .Include(c => c.Patient)
            .FirstOrDefaultAsync(c => c.Id == id, cancellationToken);
    }

    public async Task<InsuranceClaim> AddAsync(InsuranceClaim claim, CancellationToken cancellationToken)
    {
        await _context.InsuranceClaims.AddAsync(claim, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return claim;
    }

    public async Task UpdateAsync(InsuranceClaim claim, CancellationToken cancellationToken)
    {
        _context.InsuranceClaims.Update(claim);
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var claim = await _context.InsuranceClaims.FindAsync(new object[] { id }, cancellationToken);
        if (claim != null)
        {
            _context.InsuranceClaims.Remove(claim);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<IEnumerable<InsuranceClaim>> GetByPatientIdAsync(Guid patientId, CancellationToken cancellationToken)
    {
        return await _context.InsuranceClaims
            .Include(c => c.Patient)
            .Where(c => c.PatientId == patientId)
            .OrderByDescending(c => c.SubmissionDate)
            .ToListAsync(cancellationToken);
    }

    public async Task<int> CountByStatusAsync(string status, CancellationToken cancellationToken)
    {
        // Case-insensitive comparison if needed, but assuming consistent casing
        return await _context.InsuranceClaims
            .CountAsync(c => c.Status == status, cancellationToken);
    }

    public async Task<decimal> SumApprovedAmountAsync(CancellationToken cancellationToken)
    {
        return await _context.InsuranceClaims
            .Where(c => c.Status == "Approved")
            .SumAsync(c => c.ApprovedAmount, cancellationToken);
    }

    public async Task<int> CountActivePoliciesAsync(CancellationToken cancellationToken)
    {
        // Simple distinct count of Policy numbers for approved/active claims?
        // Or just total count? As user requested "Active Insurance Policy" stats.
        // Assuming unique policy numbers across claims represent unique active policies if they have recent activity?
        // Let's just count unique PolicyNumbers for now.
        return await _context.InsuranceClaims
            .Select(c => c.PolicyNumber)
            .Distinct()
            .CountAsync(cancellationToken);
    }
}
