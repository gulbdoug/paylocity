using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Models.Mapperly;
using Api.Services.EmployeeSrv;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class EmployeesController : ControllerBase
{
    private readonly IEmployeeService _employeeService;

    public EmployeesController(IEmployeeService employeeService)
    {
        _employeeService = employeeService;
    }

    [SwaggerOperation(Summary = "Get employee by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetEmployeeDto>>> Get(int id)
    {
        try
        {
            var employee = await _employeeService.GetEmployeeById(id);
            var result = new ApiResponse<GetEmployeeDto>
            {
                Data = employee,
                Success = true
            };
            return result;
        }
        catch (InvalidOperationException ex)
        {
            return NotFound(ex.Message);
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<List<GetEmployeeDto>>
            {
                Success = false,
                Message = "Error getting employee by id",
                Error = ex.Message
            });
        }
    }

    [SwaggerOperation(Summary = "Get all employees")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetEmployeeDto>>>> GetAll()
    {
        try
        {
            //task: use a more realistic production approach - DG: changed to use EntityFramework to simulate a database
            var employees = await _employeeService.GetAllEmployees();
            var result = new ApiResponse<List<GetEmployeeDto>>
            {
                Data = employees,
                Success = true
            };
            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<List<GetEmployeeDto>>
            {
                Success = false,
                Message = "Error getting all employees",
                Error = ex.Message
            });
        }
    }
}
