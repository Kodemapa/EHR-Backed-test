using EHR_Application.Application.Dtos;
using EHR_Application.Infrastructure.Services;
using EHR_Application.ODataFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;

namespace EHR_Application.Presentation.Controllers;

[ApiController]
[Route("api/prescriptions")]
public sealed class PrescriptionsController : ControllerBase
{
    private readonly IPrescriptionService _prescriptionService;

    public PrescriptionsController(IPrescriptionService prescriptionService)
    {
        _prescriptionService = prescriptionService;
    }

    [HttpGet]
    [EnableQuery(AllowedQueryOptions = ODataQueryService.PrescriptionQueryOptions, MaxTop = 100)]
    public async Task<ActionResult<IEnumerable<PrescriptionDto>>> GetAllAsync(CancellationToken cancellationToken)
    {
        var prescriptions = await _prescriptionService.GetAllAsync(cancellationToken);
        return Ok(prescriptions);
    }

    [HttpGet("{id:guid}", Name = "GetPrescriptionById")]
    [EnableQuery(AllowedQueryOptions = ODataQueryService.PrescriptionQueryOptions, MaxTop = 100)]
    public async Task<ActionResult<PrescriptionViewDto>> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var prescription = await _prescriptionService.GetByIdAsync(id, cancellationToken);
        return prescription == null ? NotFound() : Ok(prescription);
    }

    [HttpPost]
    public async Task<ActionResult<PrescriptionDto>> CreateAsync([FromBody] CreatePrescriptionDto dto, CancellationToken cancellationToken)
    {
        try 
        {
            var created = await _prescriptionService.CreateAsync(dto, cancellationToken);
            return CreatedAtRoute("GetPrescriptionById", new { id = created.Id }, created);
        }
        catch (Exception ex)
        {
            return BadRequest(new { message = ex.Message, details = ex.InnerException?.Message });
        }
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateAsync(Guid id, [FromBody] UpdatePrescriptionDto dto, CancellationToken cancellationToken)
    {
        var updated = await _prescriptionService.UpdateAsync(id, dto, cancellationToken);
        return updated ? NoContent() : NotFound();
    }

    [HttpPut("{id:guid}/cancel")]
    public async Task<IActionResult> CancelAsync(Guid id, [FromBody] CancelPrescriptionDto dto, CancellationToken cancellationToken)
    {
        var cancelled = await _prescriptionService.CancelAsync(id, dto, cancellationToken);
        return cancelled ? Ok(new { message = "Prescription cancelled successfully" }) : NotFound();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var deleted = await _prescriptionService.DeleteAsync(id, cancellationToken);
        return deleted ? NoContent() : NotFound();
    }

    [HttpGet("{id:guid}/print")]
    public async Task<IActionResult> PrintAsync(Guid id, CancellationToken cancellationToken)
    {
        var pdf = await _prescriptionService.PrintAsync(id, cancellationToken);
        if (pdf == null || pdf.Length == 0)
        {
            // For now return dummy message if printing not fully implemented
            return Ok(new { message = "Print functionality is under development" });
        }
        return File(pdf, "application/pdf", $"Prescription_{id}.pdf");
    }
}
