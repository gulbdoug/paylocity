using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public interface ISalaryFactory
    {
        ISalaryPaycheck CreateSalaryPaycheck(decimal grossPay, decimal netPay, List<IDeduction> deductions);
    }
}