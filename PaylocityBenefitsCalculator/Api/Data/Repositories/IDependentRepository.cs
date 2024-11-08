using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Data.Repositories
{
    public interface IDependentRepository
    {
        public Task<List<Dependent>> GetAllAsync();
        public Task<Dependent> GetDependentAsync(int id);
        public Task<List<Dependent>> GetEmployeeDependentsAsync(int employeeId);
        public Task<Dependent> GetDependentsAsync(int employeeId, int dependentId);
    }
}