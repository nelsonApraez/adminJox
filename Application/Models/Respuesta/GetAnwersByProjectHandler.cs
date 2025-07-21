using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.Features.Interfaces;
using MediatR;

namespace Application.Models.Respuesta
{
    public record GetAnwersByProject(Guid IdProyecto) : IRequest<List<QuestionsWithAnswersDto>>;


    public class GetAnwersByProjectHandler : IRequestHandler<GetAnwersByProject, List<QuestionsWithAnswersDto>>
    {
        protected readonly IRespuestaService _respuestaService;
        public GetAnwersByProjectHandler(IRespuestaService respuestaService)
        {
            _respuestaService = respuestaService;
        }

        public async Task<List<QuestionsWithAnswersDto>> Handle(GetAnwersByProject request, CancellationToken cancellationToken)
        {
            return await _respuestaService.AnswersByProject(request.IdProyecto);
        }
    }
}
