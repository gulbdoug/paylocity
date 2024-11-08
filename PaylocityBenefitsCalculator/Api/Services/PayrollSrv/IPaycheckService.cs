using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Payroll;

namespace Api.Services.PayrollSrv
{
    public interface IPaycheckService
    {
        Task<PaycheckDto> CalculatePaycheck(int employeeId);
    }
}