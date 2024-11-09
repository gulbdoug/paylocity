using Riok.Mapperly.Abstractions;

namespace Api.Models.Payroll
{
    public class SalaryPaycheck : PaycheckBase, IDeductions
    {
        public List<Deduction> Deductions { get; set; } = new List<Deduction>();

    }
}