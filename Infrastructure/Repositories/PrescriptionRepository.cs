using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EHR_Application.Infrastructure.Repositories;

public sealed class PrescriptionRepository : IPrescriptionRepository
{
    private readonly EhrDbContext _context;

    public PrescriptionRepository(EhrDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Prescription>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.Medications)
            .ToListAsync(cancellationToken);
    }

    public async Task<Prescription?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.Medications)
            .FirstOrDefaultAsync(p => p.Id == id, cancellationToken);
    }

    public async Task<Prescription> AddAsync(Prescription prescription, CancellationToken cancellationToken)
    {
        await _context.Prescriptions.AddAsync(prescription, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);
        return prescription;
    }

    // public async Task UpdateAsync(Prescription prescription, CancellationToken cancellationToken)
    // {
    //     _context.Prescriptions.Update(prescription);
    //     await _context.SaveChangesAsync(cancellationToken);
    // }

    public async Task UpdateAsync(Guid id, CancellationToken cancellationToken)
    {
        await _context.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var existing = await _context.Prescriptions.FindAsync(new object[] { id }, cancellationToken);
        if (existing != null)
        {
            _context.Prescriptions.Remove(existing);
            await _context.SaveChangesAsync(cancellationToken);
        }
    }

    public async Task<Prescription?> GetByCodeAsync(string code, CancellationToken cancellationToken)
    {
        return await _context.Prescriptions
            .Include(p => p.Patient)
            .Include(p => p.Doctor)
            .Include(p => p.Medications)
            .FirstOrDefaultAsync(p => p.PrescriptionCode == code, cancellationToken);
    }

    public async Task<string> GetLatestCodeAsync(CancellationToken cancellationToken)
    {
        var latest = await _context.Prescriptions
            .OrderByDescending(p => p.PrescriptionCode)
            .Select(p => p.PrescriptionCode)
            .FirstOrDefaultAsync(cancellationToken);
        
        return latest ?? "RX-2026-000";
    }
}
