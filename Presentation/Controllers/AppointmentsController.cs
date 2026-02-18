using EHR_Application.ODataFilters;
using EHR_Application.Application.Dtos;
using EHR_Application.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EHR_Application.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public sealed class AppointmentsController : ControllerBase
    {
        private readonly IAppointmentService _appointmentService;

        public AppointmentsController(IAppointmentService appointmentService)
        {
            _appointmentService = appointmentService;
        }

        /* ---------------- BASIC CRUD ---------------- */

        // Get all appointments with OData query option
        [HttpGet]
        // [Authorize]
        [EnableQuery(AllowedQueryOptions = ODataQueryService.AppointmentQueryOptions, MaxTop = 100)]
        public async Task<ActionResult<IEnumerable<AppointmentViewDto>>> GetAsync(CancellationToken cancellationToken)
        {
            var appointments = await _appointmentService.GetAsync(cancellationToken);
            return Ok(appointments);
        }

        // Get a single appointment by ID with OData query options
        [HttpGet("{id:guid}", Name = "GetAppointmentById")]
        [EnableQuery(AllowedQueryOptions = ODataQueryService.AppointmentQueryOptions)]
        public async Task<ActionResult<AppointmentViewDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetAsync(id, cancellationToken);
            return appointment is null ? NotFound() : Ok(appointment);
        }

        // Post method to create a new appointment
        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<AppointmentDto>> CreateAsync([FromBody] CreateAppointmentDto dto, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.CreateAsync(dto, cancellationToken);
            return CreatedAtRoute("GetAppointmentById", new { id = appointment.Id }, appointment);
        }

        // Put method to update an existing appointment
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateAppointmentDto dto, CancellationToken cancellationToken)
        {
            var updated = await _appointmentService.UpdateAsync(id, dto, cancellationToken);
            return updated ? NoContent() : NotFound();
        }

        // Delete method for removing an appointment
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _appointmentService.DeleteAsync(id, cancellationToken);
            return deleted ? NoContent() : NotFound();
        }

        // Get the view for a specific appointment by ID
        [HttpGet("{id:guid}/view")]
        public async Task<ActionResult<AppointmentViewDto>> GetViewByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var appointment = await _appointmentService.GetViewAsync(id, cancellationToken);
            return appointment is null ? NotFound() : Ok(appointment);
        }

        /* ---------------- NEW ENDPOINTS ---------------- */

        // Reschedule an appointment
        [HttpPost("{id:guid}/reschedule")]
        public async Task<IActionResult> RescheduleAsync(Guid id, [FromBody] RescheduleAppointmentDto dto, CancellationToken cancellationToken)
        {
            var rescheduled = await _appointmentService.RescheduleAsync(id, dto, cancellationToken);
            if (!rescheduled)
            {
                return NotFound();
            }

            return Ok(new { message = "Appointment rescheduled successfully" });
        }

        // Cancel an appointment
        [HttpPost("{id:guid}/cancel")]
        public async Task<IActionResult> CancelAsync(Guid id, [FromBody] CancelAppointmentDto dto, CancellationToken cancellationToken)
        {
            var cancelled = await _appointmentService.CancelAsync(id, dto, cancellationToken);
            if (!cancelled)
            {
                return NotFound();
            }

            return Ok(new { message = "Appointment cancelled successfully" });
        }

        // Get appointment status
        [HttpGet("{id:guid}/status")]
        public async Task<ActionResult<object>> GetStatusAsync(Guid id, CancellationToken cancellationToken)
        {
            var status = await _appointmentService.GetStatusAsync(id, cancellationToken);
            if (status is null)
            {
                return NotFound();
            }

            return Ok(new { status });
        }

        // Update appointment status
        [HttpPut("{id:guid}/status")]
        public async Task<IActionResult> UpdateStatusAsync(Guid id, [FromBody] UpdateAppointmentStatusDto dto, CancellationToken cancellationToken)
        {
            var updated = await _appointmentService.UpdateStatusAsync(id, dto, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }

            return Ok(new { message = "Appointment status updated successfully" });
        }

        // Update appointment date/time
        [HttpPut("{id:guid}/datetime")]
        public async Task<IActionResult> UpdateDateTimeAsync(Guid id, [FromBody] UpdateAppointmentDateTimeDto dto, CancellationToken cancellationToken)
        {
            var updated = await _appointmentService.UpdateDateTimeAsync(id, dto, cancellationToken);
            if (!updated)
            {
                return NotFound();
            }

            return Ok(new { message = "Appointment date/time updated successfully" });
        }
    }
}
