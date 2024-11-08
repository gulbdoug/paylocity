using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Dependent;

namespace Api.Dtos.Employee
{
    public class CreateEmployeeDto
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public decimal Salary { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<CreateDependentDto> Dependents { get; set; } = new List<CreateDependentDto>();
    }
}