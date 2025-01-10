using BLL.Feature.Command;
using MediatR;


namespace BLL.Feature.Handle
{
    public class UpdateEmployeeCommandHandler : IRequestHandler<UpdateEmployeeCommand, bool>
    {
        private readonly IEmployeeService _employeeService;

        public UpdateEmployeeCommandHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<bool> Handle(UpdateEmployeeCommand request, CancellationToken cancellationToken)
        {
            await _employeeService.UpdateEmployeeAsync(request.Employee);
            return true;
        }
    }
}
