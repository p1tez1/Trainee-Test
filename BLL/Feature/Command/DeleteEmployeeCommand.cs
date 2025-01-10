using DAL.Mode;
using MediatR;

namespace BLL.Feature.Command
{
    public record DeleteEmployeeCommand(Employee Employee) : IRequest<bool>;
}