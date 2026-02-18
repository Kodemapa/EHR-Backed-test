using EHR_Application.Domain.Models;
using EHR_Application.Application.Dtos;

namespace EHR_Application.Application.Mappings;

public static class AppointmentMappings
{
    public static AppointmentDto ToDto(this Appointment entity)
    {
        return new AppointmentDto
        {
            Id = entity.Id,
            PatientId = entity.PatientId,
            DoctorId = entity.DoctorId,
            DateTime = entity.DateTime,
            AppointmentType = entity.AppointmentType,
            Status = entity.Status,
            TokenNo = entity.TokenNo,
            Prescription = entity.Prescription,
            Reschedule = entity.Reschedule,
            Concellation = entity.Concellation
        };
    }

    public static AppointmentViewDto ToViewDto(this Appointment entity)
    {
        return new AppointmentViewDto
        {
            Id = entity.Id,
            PatientId = entity.PatientId,
            PatientName = entity.Patient != null ? $"{entity.Patient.FirstName} {entity.Patient.LastName}" : string.Empty,
            DoctorId = entity.DoctorId,
            DoctorName = entity.Doctor != null ? $"{entity.Doctor.FirstName} {entity.Doctor.LastName}" : string.Empty,
            DateTime = entity.DateTime,
            AppointmentType = entity.AppointmentType,
            Status = entity.Status,
            TokenNo = entity.TokenNo,
            Prescription = entity.Prescription,
            Reschedule = entity.Reschedule,
            Concellation = entity.Concellation
        };
    }

    public static Appointment ToEntity(this CreateAppointmentDto dto)
    {
        return new Appointment
        {
            Id = Guid.NewGuid(),
            PatientId = dto.PatientId,
            DoctorId = dto.DoctorId,
            DateTime = dto.DateTime,
            AppointmentType = dto.AppointmentType,
            TokenNo = dto.TokenNo,
            Status = string.IsNullOrWhiteSpace(dto.Status) ? "Pending" : dto.Status,
            Prescription = dto.Prescription,
            Reschedule = dto.Reschedule,
            Concellation = dto.Concellation
        };
    }

    public static void UpdateFromDto(this Appointment entity, UpdateAppointmentDto dto)
    {
        entity.PatientId = dto.PatientId;
        entity.DoctorId = dto.DoctorId;
        entity.DateTime = dto.DateTime;
        entity.AppointmentType = dto.AppointmentType;
        entity.Status = dto.Status;
        entity.TokenNo = dto.TokenNo;
        entity.Prescription = dto.Prescription;
        entity.Reschedule = dto.Reschedule;
        entity.Concellation = dto.Concellation;
    }
}
