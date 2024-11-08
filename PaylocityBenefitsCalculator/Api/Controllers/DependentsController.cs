using Api.Dtos.Dependent;
using Api.Models;
using Api.Services.DependentSrv;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;

namespace Api.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DependentsController : ControllerBase
{
    private readonly IDependentService _dependentService;

    public DependentsController(IDependentService dependentService)
    {
        _dependentService = dependentService;
    }

    [SwaggerOperation(Summary = "Get dependent by id")]
    [HttpGet("{id}")]
    public async Task<ActionResult<ApiResponse<GetDependentDto>>> Get(int id)
    {
        try
        {
            var dependent = await _dependentService.GetDependentById(id);

            var result = new ApiResponse<GetDependentDto>
            {
                Data = dependent,
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
            return BadRequest(new ApiResponse<List<GetDependentDto>>
            {
                Success = false,
                Message = "Error getting dependent by id",
                Error = ex.Message
            });
        }
    }

    [SwaggerOperation(Summary = "Get all dependents")]
    [HttpGet("")]
    public async Task<ActionResult<ApiResponse<List<GetDependentDto>>>> GetAll()
    {
        try
        {
            var dependents = await _dependentService.GetAllDependents();
            var result = new ApiResponse<List<GetDependentDto>>
            {
                Data = dependents,
                Success = true
            };
            return result;
        }
        catch (Exception ex)
        {
            return BadRequest(new ApiResponse<List<GetDependentDto>>
            {
                Success = false,
                Message = "Error getting all dependents",
                Error = ex.Message
            });
        }
    }
}
