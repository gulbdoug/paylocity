using System.ComponentModel.DataAnnotations;
using Api.Data;
using Api.Models;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext : DbContext, IApplicationDbContext
{
    public DbSet<Employee> Employees { get; set; }
    public DbSet<Dependent> Dependents { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }

    public static void SeedData(ApplicationDbContext context)
    {
        context.Database.Migrate();
    }

    /// <summary>
    /// Seed the inmemory data
    /// </summary>
    /// <param name="modelBuilder"></param>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // if (!Database.IsInMemory() && !Database.GetMigrations().Any())
        // {
        modelBuilder.Entity<Employee>().HasData(
        new Employee
        {
            Id = 1,
            FirstName = "LeBron",
            LastName = "James",
            Salary = 75420.99m,
            DateOfBirth = new DateTime(1984, 12, 30)
        },
        new Employee
        {
            Id = 2,
            FirstName = "Ja",
            LastName = "Morant",
            Salary = 92365.22m,
            DateOfBirth = new DateTime(1999, 8, 10)
        },
        new Employee
        {
            Id = 3,
            FirstName = "Michael",
            LastName = "Jordan",
            Salary = 143211.12m,
            DateOfBirth = new DateTime(1963, 2, 17)
        }
    );

        modelBuilder.Entity<Dependent>().HasData(
            new Dependent
            {
                Id = 1,
                FirstName = "Spouse",
                LastName = "Morant",
                Relationship = Relationship.Spouse,
                DateOfBirth = new DateTime(1998, 3, 3),
                EmployeeId = 2
            },
            new Dependent
            {
                Id = 2,
                FirstName = "Child1",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2020, 6, 23),
                EmployeeId = 2
            },
            new Dependent
            {
                Id = 3,
                FirstName = "Child2",
                LastName = "Morant",
                Relationship = Relationship.Child,
                DateOfBirth = new DateTime(2021, 5, 18),
                EmployeeId = 2
            },
            new Dependent
            {
                Id = 4,
                FirstName = "DP",
                LastName = "Jordan",
                Relationship = Relationship.DomesticPartner,
                DateOfBirth = new DateTime(1974, 1, 2),
                EmployeeId = 3
            }
        );
        //}
    }

    // public async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    // {
    //     return await base.SaveChangesAsync(cancellationToken);
    // }
}
