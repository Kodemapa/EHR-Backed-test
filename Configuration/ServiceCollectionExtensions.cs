using EHR_Application.Authentication;
using EHR_Application.Infrastructure.Data;
using EHR_Application.Infrastructure.Repositories;
using EHR_Application.Infrastructure.Services;
using EHR_Application.Interceptors;
// using EHR_Application.Repositories.Interfaces;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;



namespace EHR_Application.Configuration;    

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddScoped<AuditSaveChangesInterceptor>();
        // services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddDbContext<EhrDbContext>((provider, options) =>
        {
            options.UseInMemoryDatabase("EhrDb");
            options.AddInterceptors(provider.GetRequiredService<AuditSaveChangesInterceptor>());
        });

        services.AddScoped<IPatientRepository, PatientRepository>();
        services.AddScoped<IPatientService, PatientService>();

        // 🔹 Doctor registrations (ADD THIS)
        services.AddScoped<IDoctorRepository, DoctorRepository>();
        services.AddScoped<IDoctorService, DoctorService>();

        // Appointments ✅ (ADD THESE)
        services.AddScoped<IAppointmentService, AppointmentService>();
        services.AddScoped<IAppointmentRepository, AppointmentRepository>();

        // 🔹 LabOperator registrations
        services.AddScoped<ILabOperatorRepository, LabOperatorRepository>();
        services.AddScoped<ILabOperatorService, LabOperatorService>();

        // 🔹 Prescription registrations
        services.AddScoped<IPrescriptionRepository, PrescriptionRepository>();
        services.AddScoped<IPrescriptionService, PrescriptionService>();

        // 🔹 Insurance registrations
        services.AddScoped<IInsuranceRepository, InsuranceRepository>();
        services.AddScoped<IInsuranceService, InsuranceService>();

        // 🔹 Employee registrations
        services.AddScoped<IEmployeeRepository, EmployeeRepository>();
        services.AddScoped<IEmployeeService, EmployeeService>();


// Swagger configuration removed (Moved to Program.cs with Scalar)

        services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.Authority = configuration["Authentication:Authority"];
                options.Audience = configuration["Authentication:Audience"];
                options.RequireHttpsMetadata = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true
                };
            });

        services.AddAuthorization();

        return services;
    }
}
