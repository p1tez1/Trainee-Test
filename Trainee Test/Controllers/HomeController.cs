using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using DAL.Mode;
using BLL.Feature.Query;
using System.Diagnostics;
using Trainee_Test.Models;
using BLL.Feature.Command;

namespace Trainee_Test.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IWebHostEnvironment _appEnvironment;
        private readonly IMediator _mediator;

        public HomeController(ILogger<HomeController> logger, IWebHostEnvironment appEnvironment, IMediator mediator)
        {
            _logger = logger;
            _appEnvironment = appEnvironment;
            _mediator = mediator;
        }

        public async Task<IActionResult> Index()
        {
            return View();
        }

        public async Task<IActionResult> GetAll()
        {
            var employees = await _mediator.Send(new GetEmployeesQuery());
            return View(employees);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpPost]
        public async Task<IActionResult> Index(IFormFile uploadedFile)
        {
            if (uploadedFile == null)
            {
                ViewBag.Message = "Please select the file first to upload.";
                return View();
            }

            try
            {
                string message = await _mediator.Send(new ProcessCsvFileCommand(uploadedFile));
                ViewBag.Message = message;
            }
            catch (Exception ex)
            {
                ViewBag.Message = ex.Message;
            }

            return RedirectToAction("GetAll");
        }

        public async Task<IActionResult> Edite(Guid? id)
        {
            if (id != null)
            {
                var employee = await _mediator.Send(new GetEmployeeByIdQuery(id.Value));
                if (employee != null)
                    return View(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Edite(Employee employee)
        {
            await _mediator.Send(new UpdateEmployeeCommand(employee));
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [ActionName("Delete")]
        public async Task<IActionResult> ConfirmDelete(Guid? id)
        {
            if (id != null)
            {
                var employee = await _mediator.Send(new GetEmployeeByIdQuery(id.Value));
                if (employee != null)
                    return View(employee);
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid? id)
        {
            if (id != null)
            {
                var employee = await _mediator.Send(new GetEmployeeByIdQuery(id.Value));
                if (employee != null)
                {
                    await _mediator.Send(new DeleteEmployeeCommand(employee));
                    return RedirectToAction("GetAll");
                }
            }
            return NotFound();
        }
    }
}
