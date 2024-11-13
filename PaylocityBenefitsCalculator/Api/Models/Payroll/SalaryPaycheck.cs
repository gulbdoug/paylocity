using Riok.Mapperly.Abstractions;

namespace Api.Models.Payroll
{
    public class SalaryPaycheck : ISalaryPaycheck
    {
        public List<IDeduction> Deductions { get; set; } = new List<IDeduction>();
        public decimal GrossPay { get; set; }
        public decimal NetPay { get; set; }
        public DateOnly Date { get; set; }
        public int EmployeeId { get; set; }
    }
}