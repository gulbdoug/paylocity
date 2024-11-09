using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Repositories;
using Api.Dtos.Employee;
using Api.Models;

namespace Api.Services.EmployeeSrv
{
    public class EmployeeValidator : IValidator<GetEmployeeDto>
    {
        public EmployeeValidator() { }

        /// <summary>
        /// Validation logic for employee - currently only checks that a single spouse or domestic partner is present
        /// </summary>
        /// <param name="employee"></param>
        /// <returns></returns>
        public bool IsValid(GetEmployeeDto employee)
        {
            // Add validation logic here
            return ValidateOnlyOnePartner(employee);
        }

        private bool ValidateOnlyOnePartner(GetEmployeeDto employee)
        {
            if (employee.Dependents == null)
            {
                return true;
            }

            return employee.Dependents
                .Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner) == 1;
        }
    }
}