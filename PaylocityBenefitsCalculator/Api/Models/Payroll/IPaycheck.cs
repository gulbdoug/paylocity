using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public interface IDeductions
    {
        List<Deduction> Deductions { get; set; }
    }
}