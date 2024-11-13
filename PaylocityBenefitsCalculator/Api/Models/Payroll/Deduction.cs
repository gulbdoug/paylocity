using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public class Deduction : IDeduction
    {
        public required string Name { get; set; }
        public decimal Amount { get; set; } = 0;
    }
}