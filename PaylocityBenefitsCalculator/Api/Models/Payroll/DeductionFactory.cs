using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{


    public class DeductionFactory : IDeductionFactory
    {
        public IDeduction CreateDeduction(string name, decimal amount)
        {
            return new Deduction
            {
                Name = name,
                Amount = amount
            };
        }

        public List<IDeduction> CreateDeductions(List<IDeduction> deductions)
        {
            return new List<IDeduction>();
        }
    }
}