using BLL.Feature.Command;
using FluentValidation;

public class DeleteEmployeeCommandValidator : AbstractValidator<DeleteEmployeeCommand>
{
    public DeleteEmployeeCommandValidator()
    {
        RuleFor(x => x.Employee)
            .NotNull().WithMessage("Employee cannot be null");
    }
}
