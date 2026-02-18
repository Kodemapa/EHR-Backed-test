using EHR_Application.Domain.Models;
using EHR_Application.Application.Dtos;

namespace EHR_Application.Application.Mappings;

public static class PrescriptionMappings
{
    public static PrescriptionDto ToDto(this Prescription entity)
    {
        return new PrescriptionDto
        {
            Id = entity.Id,
            PrescriptionId = entity.PrescriptionCode,
            PatientName = entity.Patient != null ? $"{entity.Patient.FirstName} {entity.Patient.LastName}" : string.Empty,
            DoctorName = entity.Doctor != null ? $"{entity.Doctor.FirstName} {entity.Doctor.LastName}" : string.Empty,
            IssueDate = entity.IssueDate,
            MedicationCount = entity.Medications.Count,
            Status = entity.Status
        };
    }

    public static PrescriptionViewDto ToViewDto(this Prescription entity)
    {
        return new PrescriptionViewDto
        {
            PrescriptionId = entity.PrescriptionCode,
            Patient = new PrescriptionPatientDto
            {
                Name = entity.Patient != null ? $"{entity.Patient.FirstName} {entity.Patient.LastName}" : string.Empty,
                Age = entity.Patient?.Age ?? 0,
                Gender = entity.Patient?.Gender ?? string.Empty
            },
            Doctor = new PrescriptionDoctorDto
            {
                Name = entity.Doctor != null ? $"{entity.Doctor.FirstName} {entity.Doctor.LastName}" : string.Empty,
                Specialization = entity.Doctor?.Specialization
            },
            Diagnosis = entity.Diagnosis,
            IssueDate = entity.IssueDate,
            Status = entity.Status,
            AdditionalInstructions = entity.AdditionalInstructions,
            Medications = entity.Medications.Select(m => m.ToDto()).ToList()
        };
    }

    public static PrescriptionMedicationDto ToDto(this PrescriptionMedication entity)
    {
        return new PrescriptionMedicationDto
        {
            MedicineName = entity.MedicineName,
            Dosage = entity.Dosage,
            Frequency = entity.Frequency,
            Duration = entity.Duration,
            Notes = entity.Notes
        };
    }

    public static Prescription ToEntity(this CreatePrescriptionDto dto, Guid patientId, Guid doctorId, string nextCode)
    {
        var prescriptionId = Guid.NewGuid();
        return new Prescription
        {
            Id = prescriptionId,
            PrescriptionCode = nextCode,
            PatientId = patientId,
            DoctorId = doctorId,
            Diagnosis = dto.Diagnosis,
            IssueDate = dto.IssueDate,
            Status = "ACTIVE",
            AdditionalInstructions = dto.AdditionalInstructions,
            Medications = dto.Medications.Select(m => m.ToEntity(prescriptionId)).ToList()
        };
    }

    public static PrescriptionMedication ToEntity(this PrescriptionMedicationDto dto, Guid prescriptionId)
    {
        return new PrescriptionMedication
        {
            PrescriptionId = prescriptionId,
            MedicineName = dto.MedicineName,
            Dosage = dto.Dosage,
            Frequency = dto.Frequency,
            Duration = dto.Duration,
            Notes = dto.Notes
        };
    }

    public static void UpdateFromDto(this Prescription entity, UpdatePrescriptionDto dto)
    {
        entity.Diagnosis = dto.Diagnosis;
        // For medications, usually we replace them or update. Replacing is simpler for now.
        entity.Medications.Clear();
        foreach (var m in dto.Medications)
        {
            entity.Medications.Add(m.ToEntity(entity.Id));
        }
    }
}
