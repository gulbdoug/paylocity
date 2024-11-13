using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{


    public class SalaryFactory : ISalaryFactory
    {
        public ISalaryPaycheck CreateSalaryPaycheck(decimal grossPay, decimal netPay, List<IDeduction> deductions)
        {
            return new SalaryPaycheck
            {
                Date = DateOnly.FromDateTime(DateTime.Today),
                GrossPay = grossPay,
                Deductions = deductions,
                NetPay = netPay,
            };
        }
    }
}