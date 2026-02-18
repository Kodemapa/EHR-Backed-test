using EHR_Application.Domain.Models;

namespace EHR_Application.Infrastructure.Repositories;

public interface IDoctorRepository
{
    IQueryable<Doctor> QueryDoctors();
    Doctor? GetDoctorById(Guid id);
    void AddDoctor(Doctor doctor);
    void RemoveDoctor(Doctor doctor);
    Task SaveDoctorAsync(CancellationToken cancellationToken);
    Doctor? GetDoctorByRegistration(string registration);
}