using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Models.Payroll
{
    public class Deduction
    {
        // DG: Future - add deduction type to datbase
        // public int Id { get; set; }
        public required string Name { get; set; }
        public decimal Amount { get; set; } = 0;
        // public AmountType Type { get; set; } = AmountType.Currency;
        //  DG - future idea -- public CurrencyCode currencyCode{ get; set; }
    }
}