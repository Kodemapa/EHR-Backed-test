using EHR_Application.Domain.Models;
using EHR_Application.Application.Dtos;

namespace EHR_Application.Application.Mappings;

public static class PatientMappings
{
    /// <summary>
    /// Converts a Patient entity to PatientDto
    /// </summary>
    public static PatientDto ToDto(this Patient patient)
    {
        return new PatientDto
        {
            Id = patient.Id,
            FullName = $"{patient.FirstName} {patient.LastName}".Trim(),
            DateOfBirth = patient.DateOfBirth,
            MedicalRecordNumber = patient.MedicalRecordNumber,
            IdProof = patient.IdProof,
            Gender = patient.Gender,
            PhoneNumber = patient.PhoneNumber,
            Email = patient.Email,
            AddressLine = patient.AddressLine,
            City = patient.City,
            State = patient.State,
            Pincode = patient.Pincode,
            Country = patient.Country,
            MedicalInfo = patient.MedicalInfo,
            Vaccines = patient.Vaccines,
            Documents = patient.Documents,
            Age = patient.Age,
            LastVisit = patient.LastVisit,
            Status = patient.Status
        };
    }

    /// <summary>
    /// Converts CreatePatientDto to Patient entity
    /// </summary>
    public static Patient ToEntity(this CreatePatientDto dto)
    {
        return new Patient
        {
            Id = Guid.NewGuid(),
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            DateOfBirth = dto.DateOfBirth,
            MedicalRecordNumber = dto.MedicalRecordNumber,
            IdProof = dto.IdProof,
            Gender = dto.Gender,
            PhoneNumber = dto.PhoneNumber,
            Email = dto.Email,
            AddressLine = dto.AddressLine,
            City = dto.City,
            State = dto.State,
            Pincode = dto.Pincode,
            Country = dto.Country,
            MedicalInfo = dto.MedicalInfo,
            Vaccines = dto.Vaccines,
            Documents = dto.Documents,
            Age = dto.Age > 0 ? dto.Age : CalculateAge(dto.DateOfBirth),
            LastVisit = dto.LastVisit,
            Status = dto.Status
        };
    }

    /// <summary>
    /// Updates an existing Patient entity with values from UpdatePatientDto
    /// </summary>
    public static void UpdateFromDto(this Patient patient, UpdatePatientDto dto)
    {
        patient.FirstName = dto.FirstName;
        patient.LastName = dto.LastName;
        patient.DateOfBirth = dto.DateOfBirth;
        patient.MedicalRecordNumber = dto.MedicalRecordNumber;
        patient.IdProof = dto.IdProof;
        patient.Gender = dto.Gender;
        patient.PhoneNumber = dto.PhoneNumber;
        patient.Email = dto.Email;
        patient.AddressLine = dto.AddressLine;
        patient.City = dto.City;
        patient.State = dto.State;
        patient.Pincode = dto.Pincode;
        patient.Country = dto.Country;
        patient.MedicalInfo = dto.MedicalInfo;
        patient.Vaccines = dto.Vaccines;
        patient.Documents = dto.Documents;
        patient.Age = dto.Age;
        patient.LastVisit = dto.LastVisit;
        patient.Status = dto.Status;
    }

    private static int CalculateAge(DateOnly dateOfBirth)
    {
        var today = DateOnly.FromDateTime(DateTime.Today);
        var age = today.Year - dateOfBirth.Year;
        if (dateOfBirth > today.AddYears(-age)) age--;
        return age;
    }
}