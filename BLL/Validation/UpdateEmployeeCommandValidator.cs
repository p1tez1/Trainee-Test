using BLL.Feature.Command;
using DAL.Mode;
using FluentValidation;

public class UpdateEmployeeCommandValidator : AbstractValidator<UpdateEmployeeCommand>
{
    public UpdateEmployeeCommandValidator()
    {
        RuleFor(x => x.Employee)
            .NotNull().WithMessage("Employee cannot be null")
            .Must(BeAValidEmployee).WithMessage("Invalid employee data");
    }

    private bool BeAValidEmployee(Employee employee)
    {
        if (employee == null) return false;
        return true;
    }
}
