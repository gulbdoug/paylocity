using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Models.Payroll;

namespace Api.Dtos.Payroll
{
    public class SalaryPaycheckDto : PaycheckDtoBase, IDeductionsDto
    {
        public List<DeductionDto> Deductions { get; set; } = new List<DeductionDto>();
    }
}