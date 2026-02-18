using System.ComponentModel.DataAnnotations;

namespace EHR_Application.Application.Dtos;

public class UpdateEmployeeCompensationDto
{
    [Required]
    [Range(0, double.MaxValue)]
    public decimal BasicPay { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Allowances { get; set; }

    [Range(0, double.MaxValue)]
    public decimal? Deductions { get; set; }

    public string? PfTaxInfo { get; set; }
}
