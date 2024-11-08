using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Payroll
{
    public class DeductionDto
    {
        public required string Name { get; set; }
        public decimal Amount { get; set; } = 0;

    }
}