using System.Threading;
using System.Threading.Tasks;
using Application.Features.Parametro.Command;
using MediatR;
using Package.Utilities.Net;

namespace DigiToolsMessage.TextMessageApplication
{
    public class TextMessageHandler : IRequestHandler<TextMessageCommand, ResponseApi>
    {
        public async Task<ResponseApi> Handle(TextMessageCommand request, CancellationToken cancellationToken)
        {
            return await Task.FromResult(TextMessageApi.BuildResponseApi(request.Status, request.Message, request.Tags, request.TypeMessage, request.Obj, request.ResourceName));
        }
    }
}
