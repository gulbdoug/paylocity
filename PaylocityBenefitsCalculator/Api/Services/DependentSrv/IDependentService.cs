using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Dependent;

namespace Api.Services.DependentSrv
{
    public interface IDependentService
    {
        Task<List<GetDependentDto>> GetAllDependents();
        Task<GetDependentDto> GetDependentById(int id);
        Task<GetDependentDto> InsertDependentAsync(CreateDependentDto dependentDto);
        Task<UpdateDependentDto> UpdateDependentAsync(UpdateDependentDto dependentDto);
    }
}