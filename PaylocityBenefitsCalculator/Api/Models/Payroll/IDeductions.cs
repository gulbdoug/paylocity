using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public interface IDeductions
    {
        List<IDeduction> Deductions { get; set; }
    }
}