using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using DAL.DBContext;
using DAL.Mode;
using Microsoft.EntityFrameworkCore;

public class EmployeeService : IEmployeeService
{
    private readonly MyDbContext _context;

    public EmployeeService(MyDbContext context)
    {
        _context = context;
    }

    public async Task<string> ProcessCsvFileAsync(IFormFile uploadedFile)
    {
        string fileExtension = Path.GetExtension(uploadedFile.FileName);

        if (fileExtension != ".csv")
        {
            return "Please select the csv file with .csv extension";
        }

        var employees = new List<Employee>();

        using (var sreader = new StreamReader(uploadedFile.OpenReadStream()))
        {
            string[] headers = sreader.ReadLine().Split(';');

            while (!sreader.EndOfStream)
            {
                string[] rows = sreader.ReadLine().Split(';');

                employees.Add(new Employee
                {
                    Name = rows[0].ToString(),
                    DateOfBirth = DateTime.Parse(rows[1]),
                    Married = bool.Parse(rows[2]),
                    Phone = rows[3].ToString(),
                    Salary = decimal.Parse(rows[4].ToString())
                });
            }
        }

        _context.Employees.AddRange(employees);
        await _context.SaveChangesAsync();

        return "File uploaded and data saved successfully.";
    }

    public async Task<List<Employee>> GetAllEmployeesAsync()
    {
        return await _context.Employees.ToListAsync();
    }

    public async Task<Employee> GetEmployeeByIdAsync(Guid id)
    {
        return await _context.Employees.FirstOrDefaultAsync(e => e.Id == id);
    }

    public async Task UpdateEmployeeAsync(Employee employee)
    {
        _context.Employees.Update(employee);
        await _context.SaveChangesAsync();
    }

    public async Task<bool> DeleteEmployeeAsync(Employee employee)
    {
        if (employee != null)
        {
            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return true;
        }
        return false;
    }
}
