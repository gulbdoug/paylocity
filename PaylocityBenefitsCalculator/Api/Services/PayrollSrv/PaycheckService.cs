using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Api.Dtos.Employee;
using Api.Extensions;
using Api.Dtos.Payroll;
using Api.Models;
using Api.Models.Mapperly;
using Api.Models.Payroll;
using Api.Data.Repositories;

namespace Api.Services.PayrollSrv
{
    public class PaycheckService : IPaycheckService
    {
        // Constants - hardcoded for now, but probably should be moved to a database or config file so it can be change easier and used in multiple places
        const decimal MonthsPerYear = 12.0m;
        const decimal PaychecksPerYear = 26.0m;
        const decimal EmployeeCostBasis = 1000.0m;
        const decimal DependentAdditionalCost = 600.0m;
        const decimal HigherEmployeeMinSalary = 80000.0m;

        const decimal HigherEmployeeAdditionalPercentage = 0.02m;
        const int HigherDependentAge = 50;
        const decimal HigherDependentCost = 200.0m;

        private readonly IEmployeeRepository _employeeRepository;

        public PaycheckService(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public async Task<SalaryPaycheckDto> CalculatePaycheck(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            var paycheckGrossSalary = CalculatePaycheckGrossPay(employee.Salary);
            List<Deduction> deductions =
            [
                CreateEmployeeBaseCostDeduction(employee.Salary),
                CalculateDependentBaseCost(employee.Dependents.Count),
                CalculateHigherSalaryEmployeeAdditionalCost(employee.Salary),
                CalculateHigherDependentAgeCost(employee.Dependents),
            ];

            var paycheckNetSalary = CalculatePaycheckNetSalary(paycheckGrossSalary, deductions);

            var paycheck = CreateEmployeeSalaryPaycheck(paycheckGrossSalary, paycheckNetSalary, deductions);

            return Mapper.Map.PaycheckToGetEmployeePaycheckDto(paycheck);
        }

        private decimal ConvertToMonthlyToPaychecksPerYearValue(decimal value)
        {
            return Math.Round(value * MonthsPerYear / PaychecksPerYear, 2);
        }

        private SalaryPaycheck CreateEmployeeSalaryPaycheck(decimal paycheckGrossSalary, decimal paycheckNetSalary, List<Deduction> deductions)
        {
            return new SalaryPaycheck
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                GrossPay = paycheckGrossSalary,
                Deductions = deductions,
                NetPay = paycheckNetSalary,
            };
        }

        private decimal CalculatePaycheckNetSalary(decimal paycheckSalary, List<Deduction> deductions)
        {
            var totalDeductions = deductions.Sum(d => d.Amount);
            return paycheckSalary - totalDeductions;
        }

        public decimal CalculatePaycheckGrossPay(decimal salary)
        {
            return Math.Round(salary / PaychecksPerYear, 2);
        }

        private Deduction CreateEmployeeBaseCostDeduction(decimal salary)
        {
            return new Deduction
            {
                Name = "Employee Base Cost",
                Amount = ConvertToMonthlyToPaychecksPerYearValue(EmployeeCostBasis),
            };
        }

        private Deduction CalculateDependentBaseCost(int dependentCount)
        {
            return new Deduction
            {
                Name = "Dependent Base Cost",
                Amount = ConvertToMonthlyToPaychecksPerYearValue(DependentAdditionalCost * dependentCount)
            };
        }

        private Deduction CalculateHigherSalaryEmployeeAdditionalCost(decimal salary)
        {
            return new Deduction
            {
                Name = "Higher Salary Employee Additional Cost",
                Amount = (salary > HigherEmployeeMinSalary) ? Math.Round(salary * HigherEmployeeAdditionalPercentage / PaychecksPerYear, 2) : 0,
            };
        }

        private Deduction CalculateHigherDependentAgeCost(ICollection<Dependent> dependents)
        {

            var higherAgeDependents = dependents.Count(d => d.DateOfBirth.Age() >= HigherDependentAge);
            return new Deduction
            {
                Name = "Higher Dependent Age Cost",
                Amount = ConvertToMonthlyToPaychecksPerYearValue(higherAgeDependents * HigherDependentCost)
            };
        }

    }
}