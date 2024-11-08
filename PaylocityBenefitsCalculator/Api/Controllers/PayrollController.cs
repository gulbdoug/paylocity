using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Employee;
using Api.Dtos.Payroll;
using Api.Models;
using Api.Services.PayrollSrv;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers
{
    /// <summary>
    /// The Payroll Controller is used to calculate employee paychecks, but can be used in the future to process any other items related to paying employees
    /// It uses the PaycheckService, but other services can be added in the future to handle other payroll items 
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class PayrollController : ControllerBase
    {
        private readonly IPaycheckService _paycheckService;

        public PayrollController(IPaycheckService paycheckService)
        {
            _paycheckService = paycheckService;
        }

        [SwaggerOperation(Summary = "Get Paycheck by employee id")]
        [HttpGet("paycheck/{id}")]
        public async Task<ActionResult<ApiResponse<PaycheckDto>>> Get(int id)
        {
            var employeePaycheck = await _paycheckService.CalculatePaycheck(id);
            var result = new ApiResponse<PaycheckDto>
            {
                Data = employeePaycheck,
                Success = true
            };
            return result;
        }
    }


}