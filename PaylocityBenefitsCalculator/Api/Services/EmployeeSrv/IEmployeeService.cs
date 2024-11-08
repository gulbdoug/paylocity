using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Employee;

namespace Api.Services.EmployeeSrv
{
    public interface IEmployeeService
    {
        Task<List<GetEmployeeDto>> GetAllEmployees();
        Task<GetEmployeeDto> GetEmployeeById(int id);
        Task<GetEmployeeDto> CreateEmployee(CreateEmployeeDto employeeDto);
        Task<GetEmployeeDto> UpdateEmployee(int id, CreateEmployeeDto employeeDto);
    }
}