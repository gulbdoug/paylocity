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

        public partial GetDependentDto DependentToGetDependentDto(Dependent dependent);

        // GEt - reverse
        public partial Dependent GetDependentDtoToDependant(GetDependentDto dependentDto);

        public partial Employee GetEmployeeDtoToEmployee(GetEmployeeDto employeeDto);


        // Post
        public partial Employee CreateEmployeeDtoToEmployee(CreateEmployeeDto employeeDto);

        public partial Dependent CreateDependentDtoToDependent(CreateDependentDto dependentDto);

        // Put
        public partial Dependent UpdateDependentDtoToDependent(UpdateDependentDto dependentDto);

        // Payroll - Paycheck
        public partial PaycheckDto PaycheckToGetEmployeePaycheckDto(Paycheck paycheck);

        // public partial Paycheck GetDependentPaycheckDtoToPaycheck(GetEmployeePaycheckDto dependent);

    }
}