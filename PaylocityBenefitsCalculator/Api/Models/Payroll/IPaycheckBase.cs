using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public interface IPaycheckBase
    {
        int EmployeeId { get; set; }
        DateOnly Date { get; set; }

        decimal GrossPay { get; set; }
        decimal NetPay { get; set; }
    }
}