using BLL.Feature.Command;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace BLL.Feature.Handle
{
    public class ProcessCsvFileCommandHandler : IRequestHandler<ProcessCsvFileCommand, string>
    {
        private readonly IEmployeeService _employeeService;

        public ProcessCsvFileCommandHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<string> Handle(ProcessCsvFileCommand request, CancellationToken cancellationToken)
        {
            return await _employeeService.ProcessCsvFileAsync(request.UploadedFile);
        }
    }
}
