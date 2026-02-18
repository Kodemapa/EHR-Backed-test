using EHR_Application.Infrastructure.Data;
using EHR_Application.Infrastructure.Repositories;
using EHR_Application.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace EHR_Application.Services
{
    public static class ServiceRegistration
    {
        // Extension method to register application-wide services
        public static void AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            // Register DbContext with PostgreSQL
            services.AddDbContext<EhrDbContext>(options =>
                options.UseNpgsql(configuration.GetConnectionString("DefaultConnection")));

            // Register repositories and services
            services.AddScoped<IPatientRepository, PatientRepository>();
            services.AddScoped<IAppointmentRepository, AppointmentRepository>();
            services.AddScoped<IInsuranceRepository, InsuranceRepository>();
            services.AddScoped<IEmployeeRepository, EmployeeRepository>();

            // Register services
            services.AddScoped<IPatientService, PatientService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<IInsuranceService, InsuranceService>();
            services.AddScoped<IEmployeeService, EmployeeService>();
        }
    }
}
