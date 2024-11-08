using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Dtos.Payroll
{
    public class PaycheckDto
    {
        public required DateOnly Date { get; set; } = new DateOnly();

        public required decimal GrossPay { get; set; }
        public required decimal NetPay { get; set; }

        public List<DeductionDto> Deductions { get; set; } = new List<DeductionDto>();
        // DG: future enhancement -- public List<AdditionDto> Additions { get; set; } = new List<AdditionDto>();
    }
}