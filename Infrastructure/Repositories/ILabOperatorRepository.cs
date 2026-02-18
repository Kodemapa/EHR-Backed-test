using EHR_Application.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EHR_Application.Infrastructure.Repositories
{
    public interface ILabOperatorRepository
    {
        Task<IEnumerable<LabOperator>> GetAllAsync();
        Task<LabOperator?> GetByIdAsync(Guid id);
        Task AddAsync(LabOperator labOperator);
        Task DeleteAsync(LabOperator labOperator);
        Task SaveAsync();
    }
}
