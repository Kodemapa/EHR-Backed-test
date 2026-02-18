namespace EHR_Application.Application.Dtos;

public sealed class AppointmentViewDto
{
    public Guid Id { get; set; }
    public Guid PatientId { get; set; }
    public string PatientName { get; set; } = string.Empty;
    public Guid? DoctorId { get; set; }
    public string DoctorName { get; set; } = string.Empty;
    public DateTime DateTime { get; set; }
    public string AppointmentType { get; set; } = string.Empty;
    public string Status { get; set; } = string.Empty;
    public int TokenNo { get; set; }
    public string? Prescription { get; set; }
    public string? Reschedule { get; set; }
    public string? Concellation { get; set; }
}
