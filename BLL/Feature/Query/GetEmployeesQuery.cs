using DAL.Mode;
using MediatR;

namespace BLL.Feature.Query
{
    public record GetEmployeesQuery() : IRequest<List<Employee>>;
}
