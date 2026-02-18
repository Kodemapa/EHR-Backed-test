using EHR_Application.Application.Dtos;
using EHR_Application.Infrastructure.Services;
using EHR_Application.ODataFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace EHR_Application.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InsuranceController : ControllerBase
{
    private readonly IInsuranceService _service;

    public InsuranceController(IInsuranceService service)
    {
        _service = service;
    }

    [HttpGet("dashboard-stats")]
    public async Task<ActionResult<DashboardStatsDto>> GetDashboardStats(CancellationToken cancellationToken)
    {
        var stats = await _service.GetDashboardStatsAsync(cancellationToken);
        return Ok(stats);
    }

    [HttpGet("claims")]
    [EnableQuery(AllowedQueryOptions = ODataQueryService.InsuranceQueryOptions, MaxTop = 100)]
    public async Task<ActionResult<IEnumerable<InsuranceClaimDto>>> GetAllClaims(CancellationToken cancellationToken)
    {
        var claims = await _service.GetAllAsync(cancellationToken);
        return Ok(claims);
    }

    [HttpGet("claims/{id:guid}")]
    [EnableQuery(AllowedQueryOptions = ODataQueryService.InsuranceQueryOptions, MaxTop = 100)]
    public async Task<ActionResult<InsuranceClaimDto>> GetClaimById(Guid id, CancellationToken cancellationToken)
    {
        var claim = await _service.GetByIdAsync(id, cancellationToken);
        if (claim == null) return NotFound();
        return Ok(claim);
    }

    [HttpPost("claims")]
    public async Task<ActionResult<InsuranceClaimDto>> CreateClaim([FromBody] CreateInsuranceClaimDto dto, CancellationToken cancellationToken)
    {
        try
        {
            var created = await _service.CreateAsync(dto, cancellationToken);
            return CreatedAtAction(nameof(GetClaimById), new { id = created.Id }, created);
        }
        catch (KeyNotFoundException ex)
        {
            return NotFound(new { error = ex.Message });
        }
    }

    [HttpPut("claims/{id:guid}")]
    public async Task<IActionResult> UpdateClaimStatus(Guid id, [FromBody] UpdateInsuranceClaimStatusDto dto, CancellationToken cancellationToken)
    {
        var success = await _service.UpdateStatusAsync(id, dto, cancellationToken);
        if (!success) return NotFound();
        return NoContent();
    }

    [HttpDelete("claims/{id:guid}")]
    public async Task<IActionResult> DeleteClaim(Guid id, CancellationToken cancellationToken)
    {
        var success = await _service.DeleteAsync(id, cancellationToken);
        if (!success) return NotFound();
        return NoContent();
    }
}
