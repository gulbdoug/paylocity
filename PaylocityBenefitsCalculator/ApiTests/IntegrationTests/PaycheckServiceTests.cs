using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Api.Data;
using Api.Data.Repositories;
using Api.Models;
using Api.Models.Payroll;
using Api.Services.PayrollSrv;
using Moq;
using Xunit;

namespace ApiTests
{
    public class PaycheckServiceTests : IntegrationTest
    {
        private Mock<IEmployeeRepository> _employeeRepositoryMock;
        private ISalaryFactory _salaryFactoryMock;
        private IPaycheckService _paycheckService;

        private IDeductionFactory _deductionFactoryMock;

        public PaycheckServiceTests()
        {
            _employeeRepositoryMock = new Mock<IEmployeeRepository>();
            _salaryFactoryMock = new SalaryFactory();
            _deductionFactoryMock = new DeductionFactory();
            _paycheckService = new PaycheckService(_employeeRepositoryMock.Object, _salaryFactoryMock, _deductionFactoryMock);
        }

        [Fact]
        public async Task CalculatePaycheck_ReturnsCorrectPaycheckDto()
        {
            // Expected Results - Example
            // 100000 / 26 = 3846.15 = gross pay bi-weekly
            // deductions 
            // employee base cost = 1000 /month  (12000 / 26) = 461.54
            // dependent base cost = 1200/ month (2 dependents @ 600 each) 1200 * 12 = 14400 / 26 = 553.85
            // higher salary employee additional cost = 200 (100000 * .02) = 2000 / 26 = 76.92
            // higher dependent age cost = 200  (1 over 50 @ 200) = 200  * 12 / 26 = 92.31
            // Total Deductions = 461.54 + 553.85 + 76.92 + 92.31  = 1184.62
            // Net Pay = 3846.15 - 1184.62 = 2661.53

            // Arrange
            var employeeId = 1;
            var employee = new Employee
            {
                Id = employeeId,
                Salary = 100000.0m,
                Dependents = new List<Dependent>
                {
                    new Dependent { DateOfBirth = new DateTime(1984, 1, 2), },
                    new Dependent { DateOfBirth = new DateTime(1969, 1, 2), },
                }
            };
            _employeeRepositoryMock.Setup(repo => repo.GetEmployeeByIdAsync(employeeId)).ReturnsAsync(employee);

            // Act
            var result = await _paycheckService.CalculatePaycheck(employeeId);

            // Assert
            Assert.Equal(3846.15m, result.GrossPay);
            Assert.Equal(2661.53m, result.NetPay);
            Assert.Equal(4, result.Deductions.Count);
            Assert.Equal("Employee Base Cost", result.Deductions[0].Name);
            Assert.Equal(461.54m, result.Deductions[0].Amount);
            Assert.Equal("Dependent Base Cost", result.Deductions[1].Name);
            Assert.Equal(553.85m, result.Deductions[1].Amount);
            Assert.Equal("Higher Salary Employee Additional Cost", result.Deductions[2].Name);
            Assert.Equal(76.92m, result.Deductions[2].Amount);
            Assert.Equal("Higher Dependent Age Cost", result.Deductions[3].Name);
            Assert.Equal(92.31m, result.Deductions[3].Amount);
        }
    }
}