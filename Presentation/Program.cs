using EHR_Application.Configuration;
using EHR_Application.Infrastructure.Data;
using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using System.Text.Json;
using System.Text.Json.Serialization;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// 🔹 Register services using the method you created (Service Registration)
builder.Services.AddApplicationServices(builder.Configuration);

// 🔹 Add Controllers + OData + JSON
builder.Services.AddControllers()
    .AddOData(opt =>
    {
        // Configure OData queries: Select, Filter, OrderBy, Expand, Count, MaxTop
        opt.Select()
           .Filter()
           .OrderBy()
           .Expand()
           .Count()
           .SetMaxTop(100);  // Set max limit for records returned
    })
    .AddJsonOptions(options =>
    {
        // Adding custom DateOnly converter for handling DateOnly fields in JSON
        options.JsonSerializerOptions.Converters.Add(new DateOnlyJsonConverter());
    });

// 🔹 CORS Configuration (Allowing all origins for development)
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()       // Allow any origin
              .AllowAnyHeader()      // Allow any header
              .AllowAnyMethod();     // Allow any HTTP methods (GET, POST, PUT, DELETE)
    });
});

// 🔹 OpenAPI Configuration
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo { Title = "EHR API", Version = "v1" });

    // JWT Auth
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter 'Bearer' [space] and then your valid token."
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });

    // DateOnly Support
    options.MapType<DateOnly>(() => new OpenApiSchema { Type = "string", Format = "date" });

    // OData support
    options.OperationFilter<EHR_Application.Swagger.ODataOperationFilter>();
});

// 🔹 Logging Configuration
builder.Logging.ClearProviders(); // Clear existing logging providers
builder.Logging.AddConsole();     // Add Console logging for debugging

var app = builder.Build();

// 🔹 Apply EF migrations at startup so the Postgres tables are created
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<EhrDbContext>();
    var logger = scope.ServiceProvider.GetRequiredService<ILogger<Program>>();
    try
    {
        logger.LogInformation("Applying database migrations.");
        // db.Database.Migrate();  // Apply any pending migrations to the database
        db.Database.EnsureCreated(); // Ensure the database is created (for In-Memory)
        logger.LogInformation("Database migrations applied successfully.");
    }
    catch (Exception ex)
    {
        logger.LogError(ex, "An error occurred while applying database migrations.");
        throw;
    }
}

// 🔹 Enable OpenAPI & Scalar in Development
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(options =>
    {
        options.RouteTemplate = "openapi/{documentName}.json";
    });

    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/openapi/v1.json", "EHR API v1");
        options.RoutePrefix = "swagger"; 
    });
    
    app.MapScalarApiReference(options =>
    {
        options.WithOpenApiRoutePattern("/openapi/v1.json");
    });
}

// 🔹 Middleware Pipeline
app.UseRouting();  // Enables routing for API endpoints
app.UseCors("AllowAll");  // Enable the CORS policy
app.UseAuthentication();  // Enable authentication
app.UseAuthorization();   // Enable authorization

// 🔹 Map controllers to routes
app.MapControllers();

app.Run(); // Run the web application

// ------------------ Helpers ------------------

// Custom JSON converter to handle DateOnly types
public class DateOnlyJsonConverter : JsonConverter<DateOnly>
{
    private const string Format = "yyyy-MM-dd";

    // Deserialize DateOnly from JSON
    public override DateOnly Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        var value = reader.GetString();
        if (DateOnly.TryParseExact(value, Format, out var date))
            return date;

        throw new JsonException($"Invalid date format. Expected {Format}.");
    }

    // Serialize DateOnly to JSON
    public override void Write(Utf8JsonWriter writer, DateOnly value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.ToString(Format));
    }
}
