using Api.Models;
using Microsoft.EntityFrameworkCore;

namespace Api.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly ApplicationDbContext _context;

        public EmployeeRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllEmployeesAsync()
        {
            return await _context.Employees
                 .Include(e => e.Dependents)
                .ToListAsync();
        }

        public async Task<Employee> GetEmployeeByIdAsync(int id)
        {
            var employee = await _context.Employees
                .Include(e => e.Dependents)
                .FirstOrDefaultAsync(e => e.Id == id);

            if (employee == null)
            {
                throw new InvalidOperationException($"Employee with ID {id} not found");
            }

            return employee;

        }

        public async Task<Employee> AddEmployeeAsync(Employee employee)
        {
            if (employee == null)
            {
                throw new ArgumentNullException(nameof(employee));
            }

            await _context.Employees.AddAsync(employee);
            var resultCount = await _context.SaveChangesAsync().ConfigureAwait(false);

            if (resultCount == 0)
            {
                throw new InvalidOperationException("Failed to add employee");
            }

            return employee;
        }
    }
}