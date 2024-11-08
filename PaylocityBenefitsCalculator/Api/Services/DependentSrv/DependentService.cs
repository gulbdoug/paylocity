using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Repositories;
using Api.Dtos.Dependent;
using Api.Models.Mapperly;

namespace Api.Services.DependentSrv
{
    public class DependentService : IDependentService
    {
        private readonly IDependentRepository _dependentRepository;
        public DependentService(IDependentRepository dependentRepository)
        {
            _dependentRepository = dependentRepository;
        }
        public async Task<List<GetDependentDto>> GetAllDependents()
        {
            var dependents = await _dependentRepository.GetAllAsync();
            var dependentDtos = dependents.Select(e => Mapper.Map.DependentToGetDependentDto(e)).ToList();
            return dependentDtos;
        }

        public async Task<GetDependentDto> GetDependentById(int id)
        {
            var dependent = await _dependentRepository.GetDependentAsync(id);
            return Mapper.Map.DependentToGetDependentDto(dependent);
        }
    }
}