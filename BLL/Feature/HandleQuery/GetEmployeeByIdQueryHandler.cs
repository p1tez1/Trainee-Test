using BLL.Feature.Query;
using DAL.Mode;
using MediatR;

namespace BLL.Feature.Handle
{
    public class GetEmployeeByIdQueryHandler : IRequestHandler<GetEmployeeByIdQuery, Employee>
    {
        private readonly IEmployeeService _employeeService;

        public GetEmployeeByIdQueryHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<Employee> Handle(GetEmployeeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _employeeService.GetEmployeeByIdAsync(request.EmployeeId);
        }
    }
}