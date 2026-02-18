using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Data;

namespace EHR_Application.Infrastructure.Repositories;

public sealed class DoctorRepository : IDoctorRepository
{
    private readonly EhrDbContext _context;

    public DoctorRepository(EhrDbContext context)
    {
        _context = context;
    }

    public IQueryable<Doctor> QueryDoctors()
        => _context.Doctors;

    public Doctor? GetDoctorById(Guid id)
        => _context.Doctors.Find(id);

    public void AddDoctor(Doctor doctor)
        => _context.Doctors.Add(doctor);

    public void RemoveDoctor(Doctor doctor)
        => _context.Doctors.Remove(doctor);

    public async Task SaveDoctorAsync(CancellationToken cancellationToken)
        => await _context.SaveChangesAsync(cancellationToken);

    public Doctor? GetDoctorByRegistration(string registration)
        => _context.Doctors.FirstOrDefault(d => d.Registration == registration);
}