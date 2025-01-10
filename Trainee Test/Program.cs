using DAL.DBContext; // ��������� DbContext
using DAL.Mode; // ��������� �����
using Microsoft.EntityFrameworkCore; // ��������� EF Core
using MediatR; // ��������� MediatR
using System.Reflection;
using BLL.Feature.Handle;
using BLL.Feature.Command; // ��������� ��� ��������� �����
using FluentValidation; // ��������� FluentValidation


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// ��������� DbContext � ������������� SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MySQLConnection")));

// ��������� FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ProcessCsvFileCommandValidator>();

// ��������� MediatR ��� ��� ��������� ������
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessCsvFileCommand).Assembly));

// ��������� Pipeline Behavior ��� ��������
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// ��������� ������ ��� ������ � ������������
builder.Services.AddScoped<IEmployeeService, EmployeeService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
