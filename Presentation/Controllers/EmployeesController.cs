using EHR_Application.Application.Dtos;
using EHR_Application.Infrastructure.Services;
using EHR_Application.ODataFilters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace EHR_Application.Presentation.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;
    private readonly ILogger<EmployeesController> _logger;

    public EmployeesController(IEmployeeService employeeService, ILogger<EmployeesController> logger)
    {
        _employeeService = employeeService;
        _logger = logger;
    }

    /// Get all employees with OData query support
    [HttpGet]
    [EnableQuery(AllowedQueryOptions = ODataQueryService.EmployeeQueryOptions, MaxTop = 100)]
    public async Task<ActionResult<IEnumerable<EmployeeDto>>> GetEmployees()
    {
        var employees = await _employeeService.GetAllEmployeesAsync();
        return Ok(employees);
    }

    
    /// Get employee by ID
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetEmployeeById(Guid id)
    {
        var employee = await _employeeService.GetEmployeeByIdAsync(id);
        if (employee == null)
        {
            return NotFound(new { message = $"Employee with ID {id} not found." });
        }

        return Ok(employee);
    }


    /// Create new employee
    [HttpPost]
    public async Task<IActionResult> CreateEmployee([FromBody] CreateEmployeeDto createDto)
    {
        try
        {
            var employee = await _employeeService.CreateEmployeeAsync(createDto);
            return CreatedAtAction(nameof(GetEmployeeById), new { id = employee.Id }, employee);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while creating the employee.");
            return StatusCode(500, new { error = "An internal server error occurred." });
        }
    }

    /// Update employee
    [HttpPut("{id:guid}")]
    public async Task<IActionResult> UpdateEmployee(Guid id, [FromBody] UpdateEmployeeDto updateDto)
    {
        try
        {
            var employee = await _employeeService.UpdateEmployeeAsync(id, updateDto);
            if (employee == null)
            {
                return NotFound(new { message = $"Employee with ID {id} not found." });
            }

            return Ok(employee);
        }
        catch (InvalidOperationException ex)
        {
            return BadRequest(new { error = ex.Message });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "An error occurred while updating the employee.");
            return StatusCode(500, new { error = "An internal server error occurred." });
        }
    }

    /// Delete employee (soft delete)
    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> DeleteEmployee(Guid id)
    {
        var result = await _employeeService.DeleteEmployeeAsync(id);
        if (!result)
        {
            return NotFound(new { message = $"Employee with ID {id} not found." });
        }

        return Ok(new { message = "Employee deleted successfully." });
    }

    /// Upload Offer Letter
    [HttpPost("{id:guid}/upload-offer-letter")]
    public async Task<IActionResult> UploadOfferLetter(Guid id, IFormFile file)
    {
        var success = await _employeeService.UploadDocumentAsync(id, "offer-letter", file);
        return success ? Ok(new { message = "Offer letter uploaded successfully" }) : BadRequest("Upload failed");
    }

    /// Upload Resume
    [HttpPost("{id:guid}/upload-resume")]
    public async Task<IActionResult> UploadResume(Guid id, IFormFile file)
    {
        var success = await _employeeService.UploadDocumentAsync(id, "resume", file);
        return success ? Ok(new { message = "Resume uploaded successfully" }) : BadRequest("Upload failed");
    }

    /// Upload ID Proof
    [HttpPost("{id:guid}/upload-id-proof")]
    public async Task<IActionResult> UploadIdProof(Guid id, IFormFile file)
    {
        var success = await _employeeService.UploadDocumentAsync(id, "id-proof", file);
        return success ? Ok(new { message = "ID proof uploaded successfully" }) : BadRequest("Upload failed");
    }
}
