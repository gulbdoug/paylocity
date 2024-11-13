using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public interface IDeduction
    {
        string Name { get; set; }
        decimal Amount { get; set; }
    }
}