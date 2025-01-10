using DAL.Mode;
using MediatR;

namespace BLL.Feature.Query
{
    public record GetEmployeeByIdQuery(Guid EmployeeId) : IRequest<Employee>;
}
