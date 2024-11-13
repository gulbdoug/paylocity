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
        private readonly ISalaryFactory _salaryFactory;
        private readonly IDeductionFactory _deductionFactory;

        public PaycheckService(IEmployeeRepository employeeRepository, ISalaryFactory salaryFactory, IDeductionFactory deductionFactory)
        {
            _employeeRepository = employeeRepository;
            _salaryFactory = salaryFactory;
            _deductionFactory = deductionFactory;
        }

        /// <summary>
        /// This is the main method that calculates the paycheck for an employee
        /// Currently it is only calculating a salary type paycheck, but could be expanded to include other types (Hourly, Commision, Contractor, etc)
        /// All other methods are private helper methods that is assumed is only needed for this method/class
        /// </summary>
        /// <param name="employeeId"></param>
        /// <returns></returns>
        public async Task<SalaryPaycheckDto> CalculatePaycheck(int employeeId)
        {
            var employee = await _employeeRepository.GetEmployeeByIdAsync(employeeId);
            var paycheckGrossSalary = CalculatePaycheckGrossPay(employee.Salary);
            List<IDeduction> deductions =
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

        /// <summary>
        /// Converts a monthly value to the number of paychecks per year
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        private decimal ConvertToMonthlyToPaychecksPerYearValue(decimal value)
        {
            return Math.Round(value * MonthsPerYear / PaychecksPerYear, 2);
        }

        /// <summary>
        /// Create the paycheck object that will be returned
        /// </summary>
        /// <param name="paycheckGrossSalary"></param>
        /// <param name="paycheckNetSalary"></param>
        /// <param name="deductions"></param>
        /// <returns></returns>
        private ISalaryPaycheck CreateEmployeeSalaryPaycheck(decimal paycheckGrossSalary, decimal paycheckNetSalary, List<IDeduction> deductions)
        {
            return _salaryFactory.CreateSalaryPaycheck(paycheckGrossSalary, paycheckNetSalary, deductions);
            // return new SalaryPaycheck
            // {
            //     Date = DateOnly.FromDateTime(DateTime.Today),
            //     GrossPay = paycheckGrossSalary,
            //     Deductions = deductions,
            //     NetPay = paycheckNetSalary,
            // };
        }

        /// <summary>
        /// Calculate the paycheck net salary - gross pay minus all deductions
        /// </summary>
        /// <param name="paycheckSalary"></param>
        /// <param name="deductions"></param>
        /// <returns></returns>
        private decimal CalculatePaycheckNetSalary(decimal paycheckSalary, List<IDeduction> deductions)
        {
            if (deductions == null || deductions.Count == 0)
            {
                return paycheckSalary;
            }
            var totalDeductions = deductions.Sum(d => d.Amount);
            return paycheckSalary - totalDeductions;
        }


        /// <summary>
        /// Gross pay broken down by paycheck (26 per year)
        /// </summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        private decimal CalculatePaycheckGrossPay(decimal salary)
        {
            return Math.Round(salary / PaychecksPerYear, 2);
        }

        /// <summary>
        /// Base Cost Deduction - get yearly value, divide by number of paychecks
        /// This assumes employee was employeed for the entire year
        /// </summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        private IDeduction CreateEmployeeBaseCostDeduction(decimal salary)
        {
            var amount = ConvertToMonthlyToPaychecksPerYearValue(EmployeeCostBasis);
            return _deductionFactory.CreateDeduction("Employee Base Cost", amount);
            // return new Deduction
            // {
            //     Name = "Employee Base Cost",
            //     Amount = ConvertToMonthlyToPaychecksPerYearValue(EmployeeCostBasis),
            // };
        }

        /// <summary>
        /// Dependent Base Cost - get yearly value, divide by number of paychecks
        /// This assumes the dependent were associated with the employee for the entire year (no new dependents during the year)
        /// </summary>
        /// <param name="dependentCount"></param>
        /// <returns></returns>
        private IDeduction CalculateDependentBaseCost(int dependentCount)
        {
            var amount = ConvertToMonthlyToPaychecksPerYearValue(DependentAdditionalCost * dependentCount);
            return _deductionFactory.CreateDeduction("Dependent Base Cost", amount);
            // return new Deduction
            // {
            //     Name = "Dependent Base Cost",
            //     Amount = ConvertToMonthlyToPaychecksPerYearValue(DependentAdditionalCost * dependentCount)
            // };
        }

        /// <summary>
        /// Higher Salary Employee Additional Cost - get yearly value, divide by number of paychecks
        /// This assumes the salary didn't go over the $80,000 threshold during the year
        /// </summary>
        /// <param name="salary"></param>
        /// <returns></returns>
        private IDeduction CalculateHigherSalaryEmployeeAdditionalCost(decimal salary)
        {
            var amount = (salary > HigherEmployeeMinSalary) ? Math.Round(salary * HigherEmployeeAdditionalPercentage / PaychecksPerYear, 2) : 0;
            return _deductionFactory.CreateDeduction("Higher Salary Employee Additional Cost", amount);
            // return new Deduction
            // {
            //     Name = "Higher Salary Employee Additional Cost",
            //     Amount = (salary > HigherEmployeeMinSalary) ? Math.Round(salary * HigherEmployeeAdditionalPercentage / PaychecksPerYear, 2) : 0,
            // };
        }

        /// <summary>
        /// Higher Dependent Age Cost - get yearly value, divide by number of paychecks
        /// This checks if the dependent is currently over the age threshold (50)
        /// But this assumed the dependent turned 50 at the beginning of the year, caclulating all months of the year
        /// Should be changed to check if the dependent turned 50 during the year and only calculate for the months the dependent was over 50
        /// </summary>
        /// <param name="dependents"></param>
        /// <returns></returns>
        private IDeduction CalculateHigherDependentAgeCost(ICollection<Dependent> dependents)
        {

            var higherAgeDependents = dependents.Count(d => d.DateOfBirth.Age() >= HigherDependentAge);
            var amount = ConvertToMonthlyToPaychecksPerYearValue(higherAgeDependents * HigherDependentCost);
            return _deductionFactory.CreateDeduction("Higher Dependent Age Cost", amount);
            // return new Deduction
            // {
            //     Name = "Higher Dependent Age Cost",
            //     Amount = ConvertToMonthlyToPaychecksPerYearValue(higherAgeDependents * HigherDependentCost)
            // };
        }

    }
}