using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public interface IDeductionFactory
    {
        IDeduction CreateDeduction(string name, decimal amount);

        List<IDeduction> CreateDeductions(List<IDeduction> deductions);
    }
}