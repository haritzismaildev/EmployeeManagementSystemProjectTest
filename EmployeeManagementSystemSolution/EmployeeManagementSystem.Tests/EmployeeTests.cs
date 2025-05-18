using System;
using System.Threading.Tasks;
using EmployeeManagementSystem.Controllers;
using EmployeeManagementSystem.Data;
using EmployeeManagementSystem.DTOs;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit;

namespace EmployeeManagementSystem.Tests
{
    public class EmployeeTests
    {
        [Fact]
        public async Task CreateEmployee_SavesToDatabase()
        {
            // Konfigurasi InMemory Database untuk unit test
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "TestDb")
                .Options;

            using (var context = new ApplicationDbContext(options))
            {
                var controller = new EmployeesController(context);
                var newEmployee = new EmployeeDTO
                {
                    FirstName = "John",
                    LastName = "Doe",
                    DateOfBirth = new DateTime(1990, 1, 1),
                    Gender = "Male",
                    Address = "123 Test Street"
                };

                var result = await controller.CreateEmployee(newEmployee);
                var createdResult = result as CreatedAtActionResult;
                Assert.NotNull(createdResult);

                // Validasi penyimpanan data
                var employee = await context.Employees.FirstOrDefaultAsync(e => e.FirstName == "John" && e.LastName == "Doe");
                Assert.NotNull(employee);
            }
        }
    }
}
