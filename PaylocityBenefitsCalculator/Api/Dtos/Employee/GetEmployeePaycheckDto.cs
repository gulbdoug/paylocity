using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Employee
{
    public class GetEmployeePaycheckDto
    {
        public int Id { get; set; }
        public decimal Salary { get; set; }
        public int EmployeeAge { get; set; }
        public List<int> DependentsAge { get; set; } = new List<int>();
    }
}