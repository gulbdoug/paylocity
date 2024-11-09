using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data.Repositories;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Models;
using Api.Models.Mapperly;
using Api.Services.EmployeeSrv;

namespace Api.Services.DependentSrv
{
    public class DependentService : IDependentService
    {
        private readonly IDependentRepository _dependentRepository;
        private readonly IEmployeeService _employeeService;
        private readonly IValidator<GetEmployeeDto> _validator;

        public DependentService(IDependentRepository dependentRepository, IEmployeeService employeeService, IValidator<GetEmployeeDto> validator)
        {
            _dependentRepository = dependentRepository;
            _employeeService = employeeService;
            _validator = validator;
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

        public async Task<GetDependentDto> InsertDependentAsync(CreateDependentDto dependentDto)
        {
            await CheckForSpouseOrPartnerAsync(dependentDto.EmployeeId, null, dependentDto.Relationship);

            var dependent = Mapper.Map.CreateDependentDtoToDependent(dependentDto);
            var createdDependent = await _dependentRepository.AddDependentAsync(dependent);
            return Mapper.Map.DependentToGetDependentDto(createdDependent);
        }

        public async Task<UpdateDependentDto> UpdateDependentAsync(UpdateDependentDto dependentDto)
        {
            // // only need to check if we are a spouse or domestic partner - if this relationship was changed
            // if (dependentDto.Relationship == Relationship.Spouse || dependentDto.Relationship == Relationship.DomesticPartner)
            // {
            //     // check if there are other spouses or domestic partners 
            //     // make sure we compare dependent ids - so we don't have same one and say it is invalid
            //     var employee = await _employeeService.GetEmployeeById(dependentDto.EmployeeId);
            //     var hasSpouseOrPartner = employee.Dependents
            //     .Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner && d.Id != dependentDto.Id) == 1;

            //     if (hasSpouseOrPartner)
            //     {
            //         throw new InvalidOperationException("Only one spouse or domestic partner is allowed");
            //     }
            // }

            await CheckForSpouseOrPartnerAsync(dependentDto.EmployeeId, dependentDto.Id, dependentDto.Relationship);

            var dependent = Mapper.Map.UpdateDependentDtoToDependent(dependentDto);
            _dependentRepository.UpdateDependentAsync(dependent);

            return dependentDto;
        }

        private async Task CheckForSpouseOrPartnerAsync(int employeeId, int? dependentId, Relationship relationship)
        {
            // only need to check if we are a spouse or domestic partner - if this relationship was changed
            if (relationship == Relationship.Spouse || relationship == Relationship.DomesticPartner)
            {
                // check if there are other spouses or domestic partners 
                // make sure we compare dependent ids - so we don't have same one and say it is invalid
                var employee = await _employeeService.GetEmployeeById(employeeId);
                var hasSpouseOrPartner = employee.Dependents
                    .Count(d => d.Relationship == Relationship.Spouse || d.Relationship == Relationship.DomesticPartner
                        && (dependentId == null || d.Id != dependentId.Value)) > 0;

                if (hasSpouseOrPartner)
                {
                    throw new InvalidOperationException("Only one spouse or domestic partner is allowed");
                }
            }
        }
    }
}