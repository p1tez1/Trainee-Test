using MediatR;
using Microsoft.AspNetCore.Http;

namespace BLL.Feature.Command
{
    public record ProcessCsvFileCommand(IFormFile UploadedFile) : IRequest<string>;
}
