using DAL.DBContext; // Імпортуємо DbContext
using DAL.Mode; // Імпортуємо моделі
using Microsoft.EntityFrameworkCore; // Імпортуємо EF Core
using MediatR; // Імпортуємо MediatR
using System.Reflection;
using BLL.Feature.Handle;
using BLL.Feature.Command; // Імпортуємо для отримання збірки
using FluentValidation; // Імпортуємо FluentValidation


var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Реєстрація DbContext з використанням SQL Server
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("MySQLConnection")));

// Реєстрація FluentValidation
builder.Services.AddValidatorsFromAssemblyContaining<ProcessCsvFileCommandValidator>();

// Реєстрація MediatR для всіх обробників запитів
builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProcessCsvFileCommand).Assembly));

// Реєстрація Pipeline Behavior для валідації
builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(ValidationBehavior<,>));

// Реєстрація сервісу для роботи з працівниками
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
