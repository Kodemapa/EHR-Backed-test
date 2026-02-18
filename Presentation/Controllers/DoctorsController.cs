using EHR_Application.ODataFilters; // Reference the ODataQueryOptions class
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
    public sealed class DoctorsController : ControllerBase
    {
        private readonly IDoctorService _doctorService;

        // Constructor to inject dependencies
        public DoctorsController(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        // Get all doctors with OData query options
        [HttpGet]
        [EnableQuery(AllowedQueryOptions = ODataQueryService.DoctorQueryOptions, MaxTop = 100)]
        public async Task<ActionResult<IEnumerable<DoctorDto>>> GetAsync(CancellationToken cancellationToken)
        {
            var doctors = await _doctorService.GetAsync(cancellationToken);
            return Ok(doctors);
        }

        // Get a single doctor by ID with OData query options
        [HttpGet("{id:guid}", Name = "GetDoctorById")]
        [EnableQuery(AllowedQueryOptions = ODataQueryService.DoctorQueryOptions)]
        public async Task<ActionResult<DoctorDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var doctor = await _doctorService.GetAsync(id, cancellationToken);
            return doctor is null ? NotFound() : Ok(doctor);
        }

        // Post method to create a new doctor (no OData query needed here)
        [HttpPost]
        public async Task<ActionResult<DoctorDto>> CreateAsync([FromBody] CreateDoctorDto dto, CancellationToken cancellationToken)
        {
            var doctor = await _doctorService.CreateAsync(dto, cancellationToken);
            return CreatedAtRoute("GetDoctorById", new { id = doctor.Id }, doctor);
        }

        // Put method to update an existing doctor (no OData query needed here)
        [HttpPut("{id:guid}")]
        public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdateDoctorDto dto, CancellationToken cancellationToken)
        {
            var updated = await _doctorService.UpdateAsync(id, dto, cancellationToken);
            return updated ? NoContent() : NotFound();
        }

        // Delete method for removing a doctor (no OData query needed here)
        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _doctorService.DeleteAsync(id, cancellationToken);
                return deleted ? NoContent() : NotFound();
            }
            catch (InvalidOperationException ex)
            {
                return Conflict(new { message = ex.Message });
            }
        }

    
        // Upload doctor document
        [HttpPost("{id:guid}/upload-document")]
        public async Task<IActionResult> UploadDocumentAsync(Guid id, IFormFile file, CancellationToken cancellationToken)
        {
            var success = await _doctorService.UploadDocumentAsync(id, file, cancellationToken);
            return success ? Ok(new { message = "Document uploaded successfully" }) : BadRequest("Upload failed");
        }
    }
}
