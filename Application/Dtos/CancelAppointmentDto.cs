namespace EHR_Application.Application.Dtos;

public sealed class CancelAppointmentDto
{
    public string CancellationReason { get; set; } = string.Empty;
}
