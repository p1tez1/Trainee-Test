using Microsoft.AspNetCore.Http;
using DAL.Mode;

public interface IEmployeeService
{
    Task<string> ProcessCsvFileAsync(IFormFile uploadedFile);
    Task<List<Employee>> GetAllEmployeesAsync();
    Task<Employee> GetEmployeeByIdAsync(Guid id);
    Task UpdateEmployeeAsync(Employee employee);
    Task<bool> DeleteEmployeeAsync(Employee employee);
}
