using Microsoft.AspNetCore.Mvc;
using Riok.Mapperly.Abstractions;

namespace Api.Models;

public class Dependent
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public DateTime DateOfBirth { get; set; }
    public Relationship Relationship { get; set; }

    [MapperIgnore]
    public int EmployeeId { get; set; }

    [MapperIgnore]
    public Employee? Employee { get; set; }
}
