using Microsoft.EntityFrameworkCore;
using EHR_Application.Domain.Models;

namespace EHR_Application.Infrastructure.Data;

public sealed class EhrDbContext : DbContext
{
    public EhrDbContext(DbContextOptions<EhrDbContext> options)
        : base(options)
    {
    }

    // 🔹 Patients table
    public DbSet<Patient> Patients => Set<Patient>();

    // 🔹 Doctors table
    public DbSet<Doctor> Doctors => Set<Doctor>();

    // 🔹 Appointments table
    public DbSet<Appointment> Appointments => Set<Appointment>();

    // 🔹 LabOperators table
    public DbSet<LabOperator> LabOperators => Set<LabOperator>();

    // 🔹 Digital Prescription tables
    public DbSet<Prescription> Prescriptions => Set<Prescription>();
    public DbSet<PrescriptionMedication> PrescriptionMedications => Set<PrescriptionMedication>();
    
    // 🔹 Insurance Claims table
    public DbSet<InsuranceClaim> InsuranceClaims => Set<InsuranceClaim>();

    // 🔹 Employees table
    public DbSet<Employee> Employees => Set<Employee>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Optional: explicit table names (recommended for PostgreSQL clarity)
        modelBuilder.Entity<Patient>().ToTable("Patients");
        modelBuilder.Entity<Doctor>().ToTable("Doctors");
        modelBuilder.Entity<Appointment>().ToTable("Appointments");
        modelBuilder.Entity<LabOperator>().ToTable("LabOperators");
        modelBuilder.Entity<Prescription>().ToTable("Prescriptions");
        modelBuilder.Entity<PrescriptionMedication>().ToTable("PrescriptionMedications");
        modelBuilder.Entity<InsuranceClaim>().ToTable("InsuranceClaims");
        modelBuilder.Entity<Employee>().ToTable("Employees");
    }
}