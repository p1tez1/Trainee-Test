using BLL.Feature.Query;
using DAL.Mode;
using MediatR;

namespace BLL.Feature.Handle
{
    public class GetEmployeesQueryHandler : IRequestHandler<GetEmployeesQuery, List<Employee>>
    {
        private readonly IEmployeeService _employeeService;

        public GetEmployeesQueryHandler(IEmployeeService employeeService)
        {
            _employeeService = employeeService;
        }

        public async Task<List<Employee>> Handle(GetEmployeesQuery request, CancellationToken cancellationToken)
        {
            return await _employeeService.GetAllEmployeesAsync();
        }
    }
}
