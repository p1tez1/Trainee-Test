using DAL.Mode;
using MediatR;

namespace BLL.Feature.Command
{
    public record UpdateEmployeeCommand(Employee Employee) : IRequest<bool>;
}