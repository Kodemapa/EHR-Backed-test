using EHR_Application.Application.Dtos;
using EHR_Application.Infrastructure.Services;
using EHR_Application.ODataFilters; // Import ODataQueryService here
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Hosting;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Threading;

namespace EHR_Application.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public sealed class PatientsController : ControllerBase
    {
        private readonly IPatientService _patientService;
        private readonly IAppointmentService _appointmentService;
        private readonly IWebHostEnvironment _environment;

        // Constructor to inject dependencies
        public PatientsController(
            IPatientService patientService,
            IAppointmentService appointmentService,
            IWebHostEnvironment environment)
        {
            _patientService = patientService;
            _appointmentService = appointmentService;
            _environment = environment;
        }

        // GET: api/patients
        [HttpGet]
        [EnableQuery(AllowedQueryOptions = ODataQueryService.PatientQueryOptions, MaxTop = 100)]  // Use PatientQueryOptions
        public async Task<ActionResult<IEnumerable<PatientDto>>> GetAsync(CancellationToken cancellationToken)
        {
            var patients = await _patientService.GetAsync(cancellationToken);
            return Ok(patients);
        }

        // GET: api/patients/{id:guid}
        [HttpGet("{id:guid}", Name = "GetPatientById")]
        [EnableQuery(AllowedQueryOptions = ODataQueryService.PatientQueryOptions, MaxTop = 100)]  // Use PatientQueryOptions
        public async Task<ActionResult<PatientDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetAsync(id, cancellationToken);
            return patient is null ? NotFound() : Ok(patient);
        }

        // POST: api/patients
        [HttpPost]
        // [Authorize]
        public async Task<ActionResult<PatientDto>> CreateAsync([FromBody] CreatePatientDto dto, CancellationToken cancellationToken)
        {
            var patient = await _patientService.CreateAsync(dto, cancellationToken);
            return CreatedAtRoute("GetPatientById", new { id = patient.Id }, patient);
        }

        // PUT: api/patients/{id:guid}
        [HttpPut("{id:guid}")]
        // [Authorize]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdatePatientDto dto, CancellationToken cancellationToken)
        {
            var updated = await _patientService.UpdateAsync(id, dto, cancellationToken);
            return updated ? NoContent() : NotFound();
        }

        // DELETE: api/patients/{id:guid}
        [HttpDelete("{id:guid}")]
        // [Authorize]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            var deleted = await _patientService.DeleteAsync(id, cancellationToken);
            return deleted ? NoContent() : NotFound();
        }

        // GET: api/patients/{id:guid}/history
        [HttpGet("{id:guid}/history")]
        
        public async Task<ActionResult<string>> GetPatientHistory(Guid id)
        {
            // TODO: Implement patient history retrieval
            var history = $"Patient history for ID: {id}";
            return Ok(history);
        }

        // GET: api/patients/{id:guid}/status
        [HttpGet("{id:guid}/status")]
        
        public async Task<ActionResult<object>> GetPatientStatus(Guid id, CancellationToken cancellationToken)
        {
            var patient = await _patientService.GetAsync(id, cancellationToken);
            if (patient is null)
            {
                return NotFound();
            }
            return Ok(new { Status = patient.Status });
        }

        // POST: api/patients/{id:guid}/upload
        [HttpPost("{id:guid}/upload")]
        public async Task<IActionResult> UploadPatientFiles(Guid id, IFormFile file, CancellationToken cancellationToken)
        {
            var success = await _patientService.UploadDocumentAsync(id, file, cancellationToken);
            return success ? Ok(new { message = "Document uploaded successfully" }) : BadRequest("Upload failed");
        }

        // GET: api/patients/{id:guid}/appointments
        [HttpGet("{id:guid}/appointments")]
        public async Task<ActionResult<IEnumerable<AppointmentDto>>> GetPatientAppointments(Guid id, CancellationToken cancellationToken)
        {
            var appointments = await _appointmentService.GetByPatientIdAsync(id, cancellationToken);
            return Ok(appointments);
        }

        // GET: api/patients/{id:guid}/reports
        [HttpGet("{id:guid}/reports")]
        // [Authorize]
        public ActionResult<IEnumerable<string>> GetPatientReports(Guid id)
        {
            // TODO: Implement patient reports retrieval
            var reports = new List<string> { $"Report 1 for patient {id}", $"Report 2 for patient {id}" };
            return Ok(reports);
        }

        // GET: api/patients/{id:guid}/vaccines
        [HttpGet("{id:guid}/vaccines")]
        public async Task<ActionResult<IEnumerable<string>>> GetPatientVaccines(Guid id)
        {
            // TODO: Implement patient vaccines retrieval
            var vaccines = new List<string> { $"Vaccine 1 for patient {id}", $"Vaccine 2 for patient {id}" };
            return Ok(vaccines);
        }
    }
}
