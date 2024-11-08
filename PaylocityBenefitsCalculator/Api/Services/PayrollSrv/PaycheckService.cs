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

        public async Task<PaycheckDto> CalculatePaycheck(int employeeId)
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

        private Paycheck CreateEmployeeSalaryPaycheck(decimal paycheckGrossSalary, decimal paycheckNetSalary, List<Deduction> deductions)
        {
            return new Paycheck
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                GrossPay = paycheckGrossSalary,
                Deductions = deductions,
                NetPay = paycheckNetSalary, //ConvertToMonthlyToPaychecksPerYearValue(paycheckNetSalary),
            };
        }

        private decimal CalculatePaycheckNetSalary(decimal paycheckSalary, List<Deduction> deductions)
        {
            var totalDeductions = deductions.Sum(d => d.Amount);
            return paycheckSalary - totalDeductions;
        }

        public decimal CalculatePaycheckGrossPay(decimal salary)
        {
            return salary / PaychecksPerYear;
        }

        private decimal CalculateMonthlyGrossPay(decimal salary)
        {
            return salary / MonthsPerYear;
        }

        private Deduction CreateEmployeeBaseCostDeduction(decimal salary)
        {
            return new Deduction
            {
                Name = "Employee Base Cost",
                Amount = ConvertToMonthlyToPaychecksPerYearValue(EmployeeCostBasis), // EmployeeCostBasis * MonthsPerYear / PaychecksPerYear,
            };
        }

        private Deduction CalculateDependentBaseCost(int dependentCount)
        {
            return new Deduction
            {
                Name = "Dependent Base Cost",
                Amount = ConvertToMonthlyToPaychecksPerYearValue(DependentAdditionalCost * dependentCount) // * MonthsPerYear / PaychecksPerYear
            };
        }

        private Deduction CalculateHigherSalaryEmployeeAdditionalCost(decimal salary)
        {
            return new Deduction
            {
                Name = "Higher Employee Additional Cost",
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