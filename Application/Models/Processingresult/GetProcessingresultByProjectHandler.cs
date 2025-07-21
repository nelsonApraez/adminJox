using System;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Interfaces;
using MediatR;

namespace Application.Models.Processingresult
{
    public record GetProcessingresultByProject(Guid IdProyecto) : IRequest<Features.Models.Dto.ProcessingresultDto>;


    public class GetProcessingresultByProjectHandler : IRequestHandler<GetProcessingresultByProject, Features.Models.Dto.ProcessingresultDto>
    {
        protected readonly IProcessingresultService _processingresultService;
        public GetProcessingresultByProjectHandler(IProcessingresultService processingresultService)
        {
            _processingresultService = processingresultService;
        }

        public async Task<Features.Models.Dto.ProcessingresultDto> Handle(GetProcessingresultByProject request, CancellationToken cancellationToken)
        {
            return await _processingresultService.GetProcessingresultByProject(request.IdProyecto);
        }
    }

}
