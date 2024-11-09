using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Dtos;
using Api.Dtos.Dependent;
using Api.Dtos.Employee;
using Api.Dtos.Payroll;
using Api.Models.Payroll;
using Riok.Mapperly.Abstractions;

namespace Api.Models.Mapperly
{

    public static class Mapper
    {
        public static readonly MapperlyMapper Map = new MapperlyMapper();
    }
    [Mapper]
    public partial class MapperlyMapper
    {
        // Get
        public partial GetEmployeeDto EmployeeToGetEmployeeDto(Employee employee);

        [MapperIgnoreSource(nameof(Dependent.EmployeeId))]
        public partial GetDependentDto DependentToGetDependentDto(Dependent dependent);

        // GEt - reverse
        [MapperIgnoreTarget(nameof(Dependent.EmployeeId))]
        public partial Dependent GetDependentDtoToDependant(GetDependentDto dependentDto);

        public partial Employee GetEmployeeDtoToEmployee(GetEmployeeDto employeeDto);


        // Post
        [MapperIgnoreTarget(nameof(Dependent.Id))]
        public partial Dependent CreateDependentDtoToDependent(CreateDependentDto dependentDto);

        // Put
        public partial Dependent UpdateDependentDtoToDependent(UpdateDependentDto dependentDto);

        // Payroll - Salary Paycheck
        public partial SalaryPaycheckDto PaycheckToGetEmployeePaycheckDto(SalaryPaycheck paycheck);

    }
}