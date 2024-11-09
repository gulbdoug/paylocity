using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Payroll
{
    public class DeductionDto : IDeductionDto
    {
        public string Name { get; set; } = string.Empty;
        public decimal Amount { get; set; } = 0;
    }
}