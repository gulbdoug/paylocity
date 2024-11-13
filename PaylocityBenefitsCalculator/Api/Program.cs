using System.Reflection;
using Api.Data.Repositories;
using Api.Services.EmployeeSrv;
using Api.Services.PayrollSrv;
using Api.Services.DependentSrv;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Api.Models;
using Api.Dtos.Employee;
using Api.Models.Payroll;

AppContext.SetSwitch("Microsoft.Data.Sqlite.UseManagedRetrieval", true);

var builder = WebApplication.CreateBuilder(args);

// DG: Add DbContext for the in-memory database - for testing (sql server is better for production)
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    // DG: normally would use a SQL server or other database to store data
    options.UseInMemoryDatabase(databaseName: builder.Configuration.GetConnectionString("InMemoryDb") ?? "PaylocityInMemoryDb");
});

// Add services to the container.
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<IDependentRepository, DependentRepository>();
builder.Services.AddScoped<IEmployeeService, EmployeeService>();
builder.Services.AddScoped<IDependentService, DependentService>();
builder.Services.AddScoped<IPaycheckService, PaycheckService>();
builder.Services.AddScoped<IValidator<GetEmployeeDto>, EmployeeValidator>();
builder.Services.AddScoped<IDeductionFactory, DeductionFactory>();
builder.Services.AddScoped<ISalaryFactory, SalaryFactory>();


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.EnableAnnotations();
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Employee Benefit Cost Calculation Api",
        Description = "Api to support employee benefit cost calculations"
    });
});

var allowLocalhost = "allow localhost";
builder.Services.AddCors(options =>
{
    options.AddPolicy(allowLocalhost,
        policy => { policy.WithOrigins("http://localhost:3000", "http://localhost"); });
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetService<ApplicationDbContext>() ?? throw new InvalidOperationException("Context is null");
    context?.Database.EnsureCreated();
    // ApplicationDbContext.SeedData(context);
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors(allowLocalhost);

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
