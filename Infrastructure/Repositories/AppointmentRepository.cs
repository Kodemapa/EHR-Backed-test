using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace EHR_Application.Infrastructure.Repositories;

public sealed class AppointmentRepository : IAppointmentRepository
{
    private readonly EhrDbContext _dbContext;

    public AppointmentRepository(EhrDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<IEnumerable<Appointment>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _dbContext.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .AsNoTracking()
            .ToListAsync(cancellationToken);
    }

    public async Task<Appointment?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _dbContext.Appointments
            .Include(a => a.Patient)
            .Include(a => a.Doctor)
            .FirstOrDefaultAsync(a => a.Id == id, cancellationToken);
    }

    public async Task<Appointment> AddAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        _dbContext.Appointments.Add(appointment);
        await _dbContext.SaveChangesAsync(cancellationToken);
        return appointment;
    }

    public async Task UpdateAsync(Appointment appointment, CancellationToken cancellationToken)
    {
        _dbContext.Appointments.Update(appointment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var appointment = await GetByIdAsync(id, cancellationToken);
        if (appointment is null)
        {
            return;
        }

        _dbContext.Appointments.Remove(appointment);
        await _dbContext.SaveChangesAsync(cancellationToken);
    }
}
