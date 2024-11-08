using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos.Dependent;

namespace Api.Services.DependentSrv
{
    public interface IDependentService
    {
        public Task<List<GetDependentDto>> GetAllDependents();
        public Task<GetDependentDto> GetDependentById(int id);
    }
}