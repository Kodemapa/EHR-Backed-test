namespace EHR_Application.Application.Dtos;

public sealed class UpdateAppointmentDto
{
    public Guid PatientId { get; set; }
    public Guid? DoctorId { get; set; }
    public DateTime DateTime { get; set; }
    public string AppointmentType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int TokenNo { get; set; }
    public string? Prescription { get; set; }
    public string? Reschedule { get; set; }
    public string? Concellation { get; set; }
}
