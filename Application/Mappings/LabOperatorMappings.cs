using EHR_Application.Domain.Models;
using EHR_Application.Application.Dtos;

namespace EHR_Application.Application.Mappings;

public static class LabOperatorMappings
{
    /// <summary>
    /// Converts a LabOperator entity to LabOperatorDto
    /// </summary>
    public static LabOperatorDto ToDto(this LabOperator labOperator)
    {
        return new LabOperatorDto
        {
            Id = labOperator.Id,
            FirstName = labOperator.FirstName,
            LastName = labOperator.LastName,
            Contact = labOperator.Contact,
            OperatorId = labOperator.OperatorId,
            Role = labOperator.Role,
            AssignedLab = labOperator.AssignedLab,
            WorkSchedule = labOperator.WorkSchedule,
            LicenseNumber = labOperator.LicenseNumber,
            Qualification = labOperator.Qualification,
            Address = labOperator.Address,
            Status = labOperator.Status,
            OperatingHours = labOperator.OperatingHours,
            Experience = labOperator.Experience,
            AvailabilityStatus = labOperator.AvailabilityStatus,
            Training = labOperator.Training,
            Certification = labOperator.Certification,
            Tasks = labOperator.Tasks
        };
    }

    /// <summary>
    /// Converts CreateLabOperatorDto to LabOperator entity (for creation)
    /// </summary>
    public static LabOperator ToEntity(this CreateLabOperatorDto dto)
    {
        return new LabOperator
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Contact = dto.Contact,
            OperatorId = dto.OperatorId,
            Role = dto.Role,
            AssignedLab = dto.AssignedLab,
            WorkSchedule = dto.WorkSchedule,
            LicenseNumber = dto.LicenseNumber,
            Qualification = dto.Qualification,
            Address = dto.Address,
            Status = dto.Status,
            OperatingHours = dto.OperatingHours,
            Experience = dto.Experience,
            AvailabilityStatus = dto.AvailabilityStatus,
            Training = dto.Training,
            Certification = dto.Certification,
            Tasks = dto.Tasks
        };
    }

    /// <summary>
    /// Updates an existing LabOperator entity with values from UpdateLabOperatorDto
    /// </summary>
    public static void UpdateFromDto(this LabOperator labOperator, UpdateLabOperatorDto dto)
    {
        if (dto.FirstName != null) labOperator.FirstName = dto.FirstName;
        if (dto.LastName != null) labOperator.LastName = dto.LastName;
        if (dto.Contact != null) labOperator.Contact = dto.Contact;
        if (dto.OperatorId != null) labOperator.OperatorId = dto.OperatorId;
        if (dto.Role != null) labOperator.Role = dto.Role;
        if (dto.AssignedLab != null) labOperator.AssignedLab = dto.AssignedLab;
        if (dto.WorkSchedule != null) labOperator.WorkSchedule = dto.WorkSchedule;
        if (dto.LicenseNumber != null) labOperator.LicenseNumber = dto.LicenseNumber;
        if (dto.Qualification != null) labOperator.Qualification = dto.Qualification;
        if (dto.Address != null) labOperator.Address = dto.Address;
        if (dto.Status != null) labOperator.Status = dto.Status;
        if (dto.OperatingHours != null) labOperator.OperatingHours = dto.OperatingHours;
        if (dto.Experience != null) labOperator.Experience = dto.Experience;
        // if (dto.HealthStatus != null) labOperator.HealthStatus = dto.HealthStatus;
        if (dto.AvailabilityStatus != null) labOperator.AvailabilityStatus = dto.AvailabilityStatus;
        // if (dto.Supervisor != null) labOperator.Supervisor = dto.Supervisor;
        if (dto.Training != null) labOperator.Training = dto.Training;
        if (dto.Certification != null) labOperator.Certification = dto.Certification;
        if (dto.Tasks != null) labOperator.Tasks = dto.Tasks;
    }
}
