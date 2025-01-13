using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Mode;
using Xunit;
using Moq;

public class ProcessCsvFileAsync_ValidCsvFile_ReturnsSuccessMessage
{
    private readonly EmployeeService _employeeService;
    private readonly MyDbContext _context;

    public ProcessCsvFileAsync_ValidCsvFile_ReturnsSuccessMessage()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        _context = new MyDbContext(options);
        _employeeService = new EmployeeService(_context);
    }

    [Fact]
    public async Task Test()
    {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.FileName).Returns("file.csv");
        var content = "Name;DateOfBirth;Married;Phone;Salary\nJohn Doe;1990-01-01;true;1234567890;1000.00\n";
        var ms = new MemoryStream();
        var writer = new StreamWriter(ms);
        writer.Write(content);
        writer.Flush();
        ms.Position = 0;
        fileMock.Setup(f => f.OpenReadStream()).Returns(ms);

        var result = await _employeeService.ProcessCsvFileAsync(fileMock.Object);

        Assert.Equal("File uploaded and data saved successfully.", result);

        var employees = await _context.Employees.ToListAsync();
        Assert.Single(employees);
        Assert.Equal("John Doe", employees[0].Name);
    }
}
