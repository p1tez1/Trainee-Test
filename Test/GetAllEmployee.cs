using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Mode;
using Xunit;

public class GetAllEmployeesAsync_ReturnsEmployees
{
    private readonly EmployeeService _employeeService;

    public GetAllEmployeesAsync_ReturnsEmployees()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .EnableSensitiveDataLogging() 
            .Options;

        var context = new MyDbContext(options);
        _employeeService = new EmployeeService(context);
    }

    [Fact]
    public async Task Test()
    {
        var employees = new List<Employee>
        {
            new Employee
            {
                Name = "John Doe",
                DateOfBirth = new DateTime(1990, 1, 1),
                Married = true,
                Phone = "1234567890", 
                Salary = 1000.00m
            }
        };
        var contextField = typeof(EmployeeService).GetField("_context", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance);
        var context = (MyDbContext)contextField.GetValue(_employeeService);

        context.Employees.AddRange(employees);
        await context.SaveChangesAsync();

        var result = await _employeeService.GetAllEmployeesAsync();

        Assert.Equal(employees, result);
    }
}
