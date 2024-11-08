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

        public EmployeeService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
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

        public bool ValidateOnlyOnePartner(Employee employee)
        {
            if (employee.Dependents == null)
            {
                return true;
            }

            return employee.Dependents
                .Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner) == 1;
        }

        public async Task<GetEmployeeDto> CreateEmployee(CreateEmployeeDto employeeDto)
        {

            var employee = Mapper.Map.CreateEmployeeDtoToEmployee(employeeDto);

            if (!ValidateOnlyOnePartner(employee))
            {
                throw new InvalidOperationException("Only one spouse or domestic partner is allowed");
            }

            var createdEmployee = await _employeeRepository.AddEmployeeAsync(employee);
            return Mapper.Map.EmployeeToGetEmployeeDto(createdEmployee);
        }

        public async Task<GetEmployeeDto> UpdateEmployee(int id, CreateEmployeeDto employeeDto)
        {
            var employee = Mapper.Map.CreateEmployeeDtoToEmployee(employeeDto);
            if (!ValidateOnlyOnePartner(employee))
            {
                throw new InvalidOperationException("Only one spouse or domestic partner is allowed");
            }
            var updatedEmployee = await _employeeRepository.AddEmployeeAsync(employee);
            return Mapper.Map.EmployeeToGetEmployeeDto(updatedEmployee);
        }

        // Future - add delete
    }
}