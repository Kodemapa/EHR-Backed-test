using Microsoft.EntityFrameworkCore;
using EHR_Application.Domain.Models;
using EHR_Application.Infrastructure.Data;

namespace EHR_Application.Infrastructure.Repositories
{
    public class LabOperatorRepository : ILabOperatorRepository
    {
        private readonly EhrDbContext _context;

        public LabOperatorRepository(EhrDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LabOperator>> GetAllAsync()
        {
            return await _context.LabOperators.ToListAsync();
        }

        public async Task<LabOperator?> GetByIdAsync(Guid id)
        {
            return await _context.LabOperators.FindAsync(id);
        }

        public async Task AddAsync(LabOperator labOperator)
        {
            await _context.LabOperators.AddAsync(labOperator);
        }

        public async Task DeleteAsync(LabOperator labOperator)
        {
            _context.LabOperators.Remove(labOperator);
        }

        public async Task SaveAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
