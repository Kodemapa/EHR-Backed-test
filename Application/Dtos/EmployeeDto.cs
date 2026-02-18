namespace EHR_Application.Application.Dtos;

public class EmployeeDto
{
    public Guid Id { get; set; }
    public string EmployeeId { get; set; } = string.Empty;
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public string FullName => $"{FirstName} {LastName}";
    public string Gender { get; set; } = string.Empty;
    public DateOnly DateOfBirth { get; set; }
    public int Age
    {
        get
        {
            var today = DateTime.UtcNow;
            var age = today.Year - DateOfBirth.Year;
            if (today.Month < DateOfBirth.Month || (today.Month == DateOfBirth.Month && today.Day < DateOfBirth.Day))
                age--;
            return age;
        }
    }
    public string PhoneNumber { get; set; } = string.Empty;
    public string EmailAddress { get; set; } = string.Empty;
    public string? Address { get; set; }
    public string Department { get; set; } = string.Empty;
    public string Designation { get; set; } = string.Empty;
    public DateOnly JoiningDate { get; set; }
    public string EmploymentType { get; set; } = string.Empty;
    public decimal BasicPay { get; set; }
    public decimal? Allowances { get; set; }
    public decimal? Deductions { get; set; }
    public decimal NetPay => BasicPay + (Allowances ?? 0) - (Deductions ?? 0);
    public string? PfTaxInfo { get; set; }
    public string Status { get; set; } = string.Empty;
    public string? OfferLetter { get; set; }
    
    // New fields
    public string? Resume { get; set; }
    public string? IdProof { get; set; }
}
