using Microsoft.AspNetCore.Http;
using Moq;
using System.Threading.Tasks;
using DAL.DBContext;
using Xunit;
using Microsoft.EntityFrameworkCore;

public class ProcessCsvFileAsync_InvalidFileExtension_ReturnsErrorMessage
{
    private readonly EmployeeService _employeeService;

    public ProcessCsvFileAsync_InvalidFileExtension_ReturnsErrorMessage()
    {
        var options = new DbContextOptionsBuilder<MyDbContext>()
            .UseInMemoryDatabase(databaseName: "TestDatabase")
            .Options;

        var context = new MyDbContext(options);
        _employeeService = new EmployeeService(context);
    }

    [Fact]
    public async Task Test()
    {
        var fileMock = new Mock<IFormFile>();
        fileMock.Setup(f => f.FileName).Returns("file.txt");

        var result = await _employeeService.ProcessCsvFileAsync(fileMock.Object);

        Assert.Equal("Please select the csv file with .csv extension", result);
    }
}
