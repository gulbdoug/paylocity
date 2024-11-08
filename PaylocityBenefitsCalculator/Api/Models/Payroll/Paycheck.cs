using Riok.Mapperly.Abstractions;

namespace Api.Models.Payroll
{
    public class Paycheck
    {
        // public required string Id { get; set; }
        public required DateOnly Date { get; set; } = new DateOnly();

        // total of all additions
        public required decimal GrossPay { get; set; }
        // total of all additions minus deductions
        public required decimal NetPay { get; set; }

        public List<Deduction> Deductions { get; set; } = new List<Deduction>();
        // DG - future use  -- public List<Addition> Additions { get; set; } = new List<Addition>();

        [MapperIgnore]
        public int EmployeeId { get; set; }

        [MapperIgnore]
        public Employee? Employee { get; set; }

    }
}