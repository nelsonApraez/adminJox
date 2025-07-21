using MediatR;
using Package.Utilities.Net;

namespace Application.Features.Parametro.Command
{
    public record TextMessageCommand(int Status, EnumerationException.Message Message,
                         string[] Tags,
                         EnumerationApplication.TypeMessage TypeMessage, object Obj, string ResourceName) : IRequest<ResponseApi>;

}
