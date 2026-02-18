using EHR_Application.Application.Dtos;
using EHR_Application.Application.Mappings;
using EHR_Application.Infrastructure.Repositories;

namespace EHR_Application.Infrastructure.Services
{
    public class LabOperatorService : ILabOperatorService
    {
        private readonly ILabOperatorRepository _labOperatorRepository;

        public LabOperatorService(ILabOperatorRepository labOperatorRepository)
        {
            _labOperatorRepository = labOperatorRepository;
        }

        public async Task<IEnumerable<LabOperatorDto>> GetLabOperatorsAsync()
        {
            var operators = await _labOperatorRepository.GetAllAsync();
            return operators.Select(op => op.ToDto());
        }

        public async Task<LabOperatorDto?> GetLabOperatorByIdAsync(Guid id)
        {
            var labOperator = await _labOperatorRepository.GetByIdAsync(id);
            return labOperator?.ToDto();
        }

        public async Task<LabOperatorDto> CreateLabOperatorAsync(CreateLabOperatorDto labOperatorDto)
        {
            var labOperator = labOperatorDto.ToEntity();

            await _labOperatorRepository.AddAsync(labOperator);
            await _labOperatorRepository.SaveAsync();
            
            return labOperator.ToDto();
        }

        public async Task<bool> UpdateLabOperatorAsync(Guid id, UpdateLabOperatorDto labOperatorDto)
        {
            var labOperator = await _labOperatorRepository.GetByIdAsync(id);
            if (labOperator == null) return false;

            labOperator.UpdateFromDto(labOperatorDto);

            await _labOperatorRepository.SaveAsync();
            return true;
        }

        public async Task<bool> DeleteLabOperatorAsync(Guid id)
        {
            var labOperator = await _labOperatorRepository.GetByIdAsync(id);
            if (labOperator == null) return false;

            await _labOperatorRepository.DeleteAsync(labOperator);
            await _labOperatorRepository.SaveAsync();
            return true;
        }

        public Task<IEnumerable<string>> GetAppointmentsAsync(Guid id)
        {
            throw new NotImplementedException("Appointment retrieval for Lab Operators is not yet implemented.");
        }

        public async Task<string?> GetAssignedLabAsync(Guid id)
        {
            var labOperator = await _labOperatorRepository.GetByIdAsync(id);
            if (labOperator == null) return null;

            return string.IsNullOrEmpty(labOperator.AssignedLab) ? "Not Assigned" : labOperator.AssignedLab;
        }
    }
}
