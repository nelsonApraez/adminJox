using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using Application.Features.Interfaces;
using Application.Features.Models.Dto;
using Domain.Services.Interfaces;
using MediatR;

namespace Application.Models.Processingresult
{
    public record ProcessQuestionsAndAnswers(Guid IdProyecto) : IRequest<bool>;


    public class ProcessQuestionsAndAnswersHandler : IRequestHandler<ProcessQuestionsAndAnswers, bool>
    {
        protected readonly IProcessingresultService _processingresultService;
        protected readonly IPromptService _promptService;
        protected readonly IRespuestaService _respuestaService;
        protected readonly IragIAServices _ragService;
        protected readonly IBaseApplicationHelper<Domain.AggregateModels.Processingresult> _serviceApplication;

        public ProcessQuestionsAndAnswersHandler(IProcessingresultService processingresultService, IPromptService promptService, IRespuestaService respuestaService, IragIAServices ragService,
            IBaseApplicationHelper<Domain.AggregateModels.Processingresult> serviceApplication)
        {
            _processingresultService = processingresultService;
            _promptService = promptService;
            _respuestaService = respuestaService;
            _ragService = ragService;
            _serviceApplication = serviceApplication;
        }


        public async Task<bool> Handle(ProcessQuestionsAndAnswers request, CancellationToken cancellationToken)
        {
            const string SOLUCION = "Solucion";
            const string BENEFICIOS = "Beneficios";
            const string ESTRATEGIA = "Estrategia";

            string questionsAndAnswersString = string.Empty;

            var result = await _promptService.ToListAsync();
            var promtSolution = result.Where(x => x.Nombre.Valor == SOLUCION).FirstOrDefault().Promtuser.Valor;
            var promtBenefit = result.Where(x => x.Nombre.Valor == BENEFICIOS).FirstOrDefault().Promtuser.Valor;
            var promtStrategy = result.Where(x => x.Nombre.Valor == ESTRATEGIA).FirstOrDefault().Promtuser.Valor;

            var questionsAndAnswers = await _respuestaService.AnswersByProject(request.IdProyecto);

            foreach (var question in questionsAndAnswers)
            {
                questionsAndAnswersString = questionsAndAnswersString + question.Pregunta + " " + question.Repuesta + ", ";
            }

            promtSolution = promtSolution.Replace("{0}", questionsAndAnswersString);

            var iaResponseSuggestedSolution = await _ragService.GetMessageRagAsync(promtSolution, result.Where(x => x.Nombre.Valor == SOLUCION).FirstOrDefault());

            if (iaResponseSuggestedSolution == null)
                return false ;

            promtBenefit = promtBenefit.Replace("{0}", iaResponseSuggestedSolution);

            var iaResponseBenefitCalculation = await _ragService.GetMessageRagAsync(promtBenefit, result.Where(x => x.Nombre.Valor == BENEFICIOS).FirstOrDefault());

            promtStrategy = promtStrategy.Replace("{0}", iaResponseSuggestedSolution);

            var iaResponseAccompanyingStrategy = await _ragService.GetMessageRagAsync(promtStrategy, result.Where(x => x.Nombre.Valor == ESTRATEGIA).FirstOrDefault());

            var response = await _processingresultService.GetProcessingresultByProject(request.IdProyecto);

            ProcessingresultDto processingresultDto = new();

            if (response == null)
            {
                processingresultDto = new()
                {
                    Proyectoid = request.IdProyecto.ToString(),
                    Suggestedsolution = iaResponseSuggestedSolution,
                    Benefitcalculation = iaResponseBenefitCalculation,
                    Accompanyingstrategy = iaResponseAccompanyingStrategy
                };
                var objEntity = _serviceApplication.MapObj<ProcessingresultDto, Domain.AggregateModels.Processingresult>(processingresultDto);
                await _processingresultService.CreateAsync(objEntity);
            }
            else
            {
                processingresultDto = new()
                {
                    Id = response.Id,
                    Proyectoid = request.IdProyecto.ToString(),
                    Suggestedsolution = iaResponseSuggestedSolution,
                    Benefitcalculation = iaResponseBenefitCalculation,
                    Accompanyingstrategy = iaResponseAccompanyingStrategy
                };
                var objEntity = _serviceApplication.MapObj<ProcessingresultDto, Domain.AggregateModels.Processingresult>(processingresultDto);
                await _processingresultService.EditAsync(objEntity);
            }

            return true;
        }
    }
}
