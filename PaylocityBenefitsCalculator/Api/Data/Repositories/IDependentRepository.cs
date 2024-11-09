using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models;

namespace Api.Data.Repositories
{
    public interface IDependentRepository
    {
        Task<List<Dependent>> GetAllAsync();
        Task<Dependent> GetDependentAsync(int id);
        Task<List<Dependent>> GetEmployeeDependentsAsync(int employeeId);
        Task<Dependent> GetDependentsAsync(int employeeId, int dependentId);

        Task<Dependent> AddDependentAsync(Dependent employee);
        void UpdateDependentAsync(Dependent employee);
    }
}