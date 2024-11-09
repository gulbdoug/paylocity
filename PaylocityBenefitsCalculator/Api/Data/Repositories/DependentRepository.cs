using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class DependentRepository : IDependentRepository
    {
        private readonly ApplicationDbContext _context;
        public DependentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Dependent>> GetAllAsync()
        {
            return await _context.Dependents.ToListAsync();
        }

        public async Task<Dependent> GetDependentAsync(int id)
        {
            var dependent = await _context.Dependents
           .FirstOrDefaultAsync(e => e.Id == id);

            if (dependent == null)
            {
                throw new InvalidOperationException($"Dependent with ID {id} not found");
            }

            return dependent;
        }

        public async Task<List<Dependent>> GetEmployeeDependentsAsync(int employeeId)
        {
            return await _context.Dependents
                 .Where(x => x.EmployeeId == employeeId)
                 .ToListAsync();
        }

        public async Task<Dependent> GetDependentsAsync(int employeeId, int dependentId)
        {
            var dependent = await _context.Dependents
                .Where(x => x.EmployeeId == employeeId && x.Id == dependentId)
                .FirstOrDefaultAsync();

            if (dependent == null)
            {
                throw new InvalidOperationException($"Dependent with ID {dependentId} not found");
            }

            return dependent;
        }

        public async Task<Dependent> AddDependentAsync(Dependent dependent)
        {
            if (dependent == null)
            {
                throw new ArgumentNullException(nameof(dependent));
            }

            await _context.Dependents.AddAsync(dependent);
            var resultCount = await _context.SaveChangesAsync().ConfigureAwait(false);

            if (resultCount == 0)
            {
                throw new InvalidOperationException("Failed to add employee");
            }

            return dependent;
        }

        public async void UpdateDependentAsync(Dependent dependent)
        {
            if (dependent == null)
            {
                throw new ArgumentNullException(nameof(dependent));
            }
            _context.Dependents.Update(dependent);
            var resultCount = await _context.SaveChangesAsync().ConfigureAwait(false);

            if (resultCount == 0)
            {
                throw new InvalidOperationException("Failed to update Dependent");
            }
        }
    }
}