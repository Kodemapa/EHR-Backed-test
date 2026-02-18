using EHR_Application.Domain.Models;
using EHR_Application.Application.Dtos;

namespace EHR_Application.Application.Mappings;

public static class DoctorMappings
{
    /// <summary>
    /// Converts a Doctor entity to DoctorDto
    /// </summary>
    public static DoctorDto ToDto(this Doctor doctor)
    {
        return new DoctorDto
        {
            Id = doctor.Id,
            // Basic Information
            Title = doctor.Title,
            FirstName = doctor.FirstName,
            LastName = doctor.LastName,
            Gender = doctor.Gender,
            DateOfBirth = doctor.DateOfBirth,

            // Professional Information
            Specialization = doctor.Specialization,
            Degree = doctor.Degree,
            Qualification = doctor.Qualification,
            ExperienceYears = doctor.ExperienceYears,
            Registration = doctor.Registration,
            PF = doctor.PF,

            // Contact Information
            Email = doctor.Email,
            PhoneNumber = doctor.PhoneNumber,
            PhoneResidence = doctor.PhoneResidence,
            OfficeNumber = doctor.OfficeNumber,

            // Location Information
            NameOfCentre = doctor.NameOfCentre,
            Address = doctor.Address,
            Area = doctor.Area,
            City = doctor.City,
            Pincode = doctor.Pincode,

            // Document
            Document = doctor.Document,

            // Status
            IsActive = doctor.IsActive
        };
    }

    /// <summary>
    /// Converts CreateDoctorDto to Doctor entity
    /// </summary>
    public static Doctor ToEntity(this CreateDoctorDto dto)
    {
        return new Doctor
        {
            Id = Guid.NewGuid(),
            // Basic Information
            Title = dto.Title,
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Gender = dto.Gender,
            DateOfBirth = dto.DateOfBirth,

            // Professional Information
            Specialization = dto.Specialization,
            Degree = dto.Degree,
            Qualification = dto.Qualification,
            ExperienceYears = dto.ExperienceYears,
            Registration = dto.Registration,
            PF = dto.PF,

            // Contact Information
            Email = dto.Email,
            PhoneNumber = dto.PhoneNumber,
            PhoneResidence = dto.PhoneResidence,
            OfficeNumber = dto.OfficeNumber,

            // Location Information
            NameOfCentre = dto.NameOfCentre,
            Address = dto.Address,
            Area = dto.Area,
            City = dto.City,
            Pincode = dto.Pincode,

            // Document
            Document = dto.Document,

            // Status
            IsActive = true
        };
    }

    /// <summary>
    /// Updates an existing Doctor entity with values from UpdateDoctorDto
    /// </summary>
    public static void UpdateFromDto(this Doctor doctor, UpdateDoctorDto dto)
    {
        // Basic Information
        if (dto.Title != null) doctor.Title = dto.Title;
        if (dto.FirstName != null) doctor.FirstName = dto.FirstName;
        if (dto.LastName != null) doctor.LastName = dto.LastName;
        if (dto.Gender != null) doctor.Gender = dto.Gender;
        if (dto.DateOfBirth.HasValue) doctor.DateOfBirth = dto.DateOfBirth.Value;

        // Professional Information
        if (dto.Specialization != null) doctor.Specialization = dto.Specialization;
        if (dto.Degree != null) doctor.Degree = dto.Degree;
        if (dto.Qualification != null) doctor.Qualification = dto.Qualification;
        if (dto.ExperienceYears.HasValue) doctor.ExperienceYears = dto.ExperienceYears.Value;
        if (dto.Registration != null) doctor.Registration = dto.Registration;
        if (dto.PF != null) doctor.PF = dto.PF;

        // Contact Information
        if (dto.Email != null) doctor.Email = dto.Email;
        if (dto.PhoneNumber != null) doctor.PhoneNumber = dto.PhoneNumber;
        if (dto.PhoneResidence != null) doctor.PhoneResidence = dto.PhoneResidence;
        if (dto.OfficeNumber != null) doctor.OfficeNumber = dto.OfficeNumber;

        // Location Information
        if (dto.NameOfCentre != null) doctor.NameOfCentre = dto.NameOfCentre;
        if (dto.Address != null) doctor.Address = dto.Address;
        if (dto.Area != null) doctor.Area = dto.Area;
        if (dto.City != null) doctor.City = dto.City;
        if (dto.Pincode != null) doctor.Pincode = dto.Pincode;

        // Document
        if (dto.Document != null) doctor.Document = dto.Document;

        // Status
        if (dto.IsActive.HasValue) doctor.IsActive = dto.IsActive.Value;
    }
}
