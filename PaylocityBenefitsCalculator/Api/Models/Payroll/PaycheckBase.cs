using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public class PaycheckBase
    {
        public int EmployeeId { get; set; }
        public DateOnly Date { get; set; }

        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
    }
}