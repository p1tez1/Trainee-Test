using BLL.Feature.Command;
using FluentValidation;
using Microsoft.AspNetCore.Http;

public class ProcessCsvFileCommandValidator : AbstractValidator<ProcessCsvFileCommand>
{
    public ProcessCsvFileCommandValidator()
    {
        RuleFor(x => x.UploadedFile)
            .NotNull().WithMessage("Uploaded file cannot be null")
            .Must(BeAValidFile).WithMessage("Please select a valid csv file with .csv extension");
    }

    private bool BeAValidFile(IFormFile file)
    {
        if (file == null) return false;
        return Path.GetExtension(file.FileName).ToLower() == ".csv";
    }
}
