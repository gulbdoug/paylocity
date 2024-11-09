using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Services.EmployeeSrv
{
    public interface IValidator<T>
    {
        bool IsValid(T obj);
    }
}