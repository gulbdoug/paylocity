using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Data.Repositories
{
    public interface IEmployeeRepository
    {
        public Task<List<Employee>> GetAllEmployeesAsync();
        public Task<Employee> GetEmployeeByIdAsync(int id);
        public Task<Employee> AddEmployeeAsync(Employee employee);
    }
}