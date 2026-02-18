using EHR_Application.Application.Dtos;  // <-- Make sure this is included at the top

namespace EHR_Application.Infrastructure.Services
{
    public interface ILabOperatorService
    {
        // Fetch all lab operators
        Task<IEnumerable<LabOperatorDto>> GetLabOperatorsAsync();

        // Fetch a lab operator by ID
        Task<LabOperatorDto?> GetLabOperatorByIdAsync(Guid id);

        // Add a new lab operator
        Task<LabOperatorDto> CreateLabOperatorAsync(CreateLabOperatorDto labOperatorDto);

        // Update an existing lab operator
        Task<bool> UpdateLabOperatorAsync(Guid id, UpdateLabOperatorDto labOperatorDto);

        // Delete a lab operator
        Task<bool> DeleteLabOperatorAsync(Guid id);

        // Get appointments
        Task<IEnumerable<string>> GetAppointmentsAsync(Guid id);

        // Get assigned lab
        Task<string?> GetAssignedLabAsync(Guid id);
    }
}
