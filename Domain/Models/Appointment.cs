using System.ComponentModel.DataAnnotations.Schema;

namespace EHR_Application.Domain.Models;

public sealed class Appointment
{
    public Guid Id { get; set; }

    [Column("Patient Id")]
    public Guid PatientId { get; set; }

    [Column("Doctor Id")]
    public Guid? DoctorId { get; set; }

    [Column("Date & Time")]
    public DateTime DateTime { get; set; }

    [Column("Appointment type")]
    public string AppointmentType { get; set; } = string.Empty;

    [Column("Status")]
    public string Status { get; set; } = "Pending";

    [Column("Token no:")]
    public int TokenNo { get; set; }

    [Column("Prescription")]
    public string? Prescription { get; set; }

    [Column("Reschedule")]
    public string? Reschedule { get; set; } // Using string as it might be a date/time string or reason

    [Column("Concellation")]
    public string? Concellation { get; set; }

    // Navigation properties
    public Patient? Patient { get; set; }
    public Doctor? Doctor { get; set; }
}
