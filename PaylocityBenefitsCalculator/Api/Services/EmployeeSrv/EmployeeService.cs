using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Repositories;
using Api.Dtos.Employee;
using Api.Models;
using Api.Models.Mapperly;

namespace Api.Services.EmployeeSrv
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository _employeeRepository;
        // private readonly IValidator<Employee> _validator;

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
            // _validator = validator;
        }

        public async Task<List<GetEmployeeDto>> GetAllEmployees()
        {
            var employees = await _employeeRepository.GetAllEmployeesAsync();
            var employeeDtos = employees.Select(e => Mapper.Map.EmployeeToGetEmployeeDto(e)).ToList();
            return employeeDtos;
        }

        public async Task<GetEmployeeDto> GetEmployeeById(int id)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(id);
            return Mapper.Map.EmployeeToGetEmployeeDto(employee);
        }

        // Future - add Insert, Update, Delete
    }
}