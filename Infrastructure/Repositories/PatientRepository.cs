using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EHR_Application.Infrastructure.Repositories;

public sealed class PatientRepository : IPatientRepository
{
    private readonly EhrDbContext _context;

    public PatientRepository(EhrDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Patient>> GetAllAsync(CancellationToken cancellationToken) =>
        await _context.Patients
            .AsNoTracking()
            .OrderBy(patient => patient.LastName)
            .ThenBy(patient => patient.FirstName)
            .ToListAsync(cancellationToken);

    public async Task<Patient?> GetByIdAsync(Guid id, CancellationToken cancellationToken) =>
        await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(patient => patient.Id == id, cancellationToken);

    public async Task<Patient> AddAsync(Patient patient, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        await _context.Patients.AddAsync(patient, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
        return patient;
    }

    public async Task UpdateAsync(Patient patient, CancellationToken cancellationToken)
    {
        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        _context.Patients.Update(patient);
        await _context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(entity => entity.Id == id, cancellationToken);
        if (patient is null)
        {
            return;
        }

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        _context.Patients.Remove(patient);
        await _context.SaveChangesAsync(cancellationToken);

        await transaction.CommitAsync(cancellationToken);
    }

    public async Task<Patient?> GetByMRNAsync(string mrn, CancellationToken cancellationToken) =>
        await _context.Patients
            .AsNoTracking()
            .FirstOrDefaultAsync(patient => patient.MedicalRecordNumber == mrn, cancellationToken);
}
