using EHR_Application.Application.Dtos;
using EHR_Application.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System;
using EHR_Application.ODataFilters;  // <-- Make sure this is included
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHR_Application.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    // [Authorize]
    public class LabOperatorsController : ControllerBase
    {
        private readonly ILabOperatorService _labOperatorService;

        public LabOperatorsController(ILabOperatorService labOperatorService)
        {
            _labOperatorService = labOperatorService;
        }

        // GET: api/lab-operators (with OData)
        [HttpGet]
        // [AllowAnonymous]
        [EnableQuery(AllowedQueryOptions = 
            ODataQueryService.LabOperatorQueryOptions, MaxTop = 100)]  // Reuse ODataQueryService
        public async Task<ActionResult<IEnumerable<LabOperatorDto>>> GetLabOperators()
        {
            var operators = await _labOperatorService.GetLabOperatorsAsync();
            return Ok(operators);
        }

        // GET: api/lab-operators/{id} (with OData)
        [HttpGet("{id}", Name = "GetLabOperatorById")]
        // [AllowAnonymous]
        [EnableQuery(AllowedQueryOptions = 
            ODataQueryService.LabOperatorQueryOptions)]  // Reuse ODataQueryService
        public async Task<ActionResult<LabOperatorDto>> GetLabOperatorById(Guid id)
        {
            var labOperator = await _labOperatorService.GetLabOperatorByIdAsync(id);
            if (labOperator == null)
            {
                return NotFound();
            }
            return Ok(labOperator);
        }

        // POST: api/lab-operators
        [HttpPost]
        // [AllowAnonymous]
        public async Task<ActionResult<LabOperatorDto>> CreateLabOperator([FromBody] CreateLabOperatorDto dto)
        {
            if (dto == null)
            {
                return BadRequest("Invalid lab operator data.");
            }

            var createdOperator = await _labOperatorService.CreateLabOperatorAsync(dto);
            return CreatedAtRoute("GetLabOperatorById", new { id = createdOperator.Id }, createdOperator);
        }

        // PUT: api/lab-operators/{id}
        [HttpPut("{id}")]
        // [AllowAnonymous]
        public async Task<IActionResult> UpdateLabOperator(Guid id, [FromBody] UpdateLabOperatorDto dto)
        {
            if (dto == null) 
            {
                return BadRequest("Invalid lab operator data.");
            }

            var updated = await _labOperatorService.UpdateLabOperatorAsync(id, dto);
            return updated ? NoContent() : NotFound();
        }

        // DELETE: api/lab-operators/{id}
        [HttpDelete("{id}")]
        // [AllowAnonymous]
        public async Task<IActionResult> DeleteLabOperator(Guid id)
        {
            var deleted = await _labOperatorService.DeleteLabOperatorAsync(id);
            return deleted ? NoContent() : NotFound();
        }

        // GET: api/lab-operators/{id}/appointments
        [HttpGet("{id}/appointments")]
        // [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<string>>> GetAppointments(Guid id)
        {
            // The service method GetAppointmentsAsync currently throws NotImplementedException.
            // Returning 501 Not Implemented to avoid a 500 error crash.
            return StatusCode(501, "Appointment retrieval for Lab Operators is not yet implemented.");
        }

        // GET: api/lab-operators/{id}/assigned-lab
        [HttpGet("{id}/assigned-lab")]
        // [AllowAnonymous]
        public async Task<ActionResult<string>> GetAssignedLab(Guid id)
        {
            var assignedLab = await _labOperatorService.GetAssignedLabAsync(id);
            if (assignedLab == null)
            {
                return NotFound();
            }
            return Ok(assignedLab);
        }

    }
}
