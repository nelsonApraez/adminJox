using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Interfaces;
using MediatR;

namespace Application.Models.Processingresult
{
    public record GetProcessingFileByProject(Guid IdProyecto) : IRequest<Byte[]>;


    public class GetProcessingFileByProjectHandler : IRequestHandler<GetProcessingFileByProject, Byte[]>
    {
        protected readonly IProcessingresultService _processingresultService;
        public GetProcessingFileByProjectHandler(IProcessingresultService processingresultService)
        {
            _processingresultService = processingresultService;
        }

        public async Task<byte[]> Handle(GetProcessingFileByProject request, CancellationToken cancellationToken)
        {
            return await _processingresultService.GetProcessingFileByProject(request.IdProyecto);
        }
    }

}
