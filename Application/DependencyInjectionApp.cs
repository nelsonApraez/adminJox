
using Application.Features.Interfaces;
using Application.Features.Models.Dto;
using Application.Features.Services;
using Domain.AggregateModels;
using Microsoft.Extensions.DependencyInjection;


namespace Application
{
    public static partial class DependencyInjection
    {

        public static IServiceCollection AddMediatrDependencyInjectionApp(this IServiceCollection services)
        {

            services.RegisterMediatrAbstractService<ProyectoService, ProyectoDto, Proyecto, IProyectoService>();
            services.RegisterMediatrAbstractService<PreguntaService, PreguntaDto, Pregunta, IPreguntaService>();
            services.RegisterMediatrAbstractService<RespuestaService, RespuestaDto, Respuesta, IRespuestaService>();

            services.RegisterMediatrAbstractService<PromptService, PromptDto, Prompt, IPromptService>();
            services.RegisterMediatrAbstractService<ProcessingresultService, ProcessingresultDto, Processingresult, IProcessingresultService>();
            services.RegisterMediatrAbstractService<ConversationService, ConversationDto, Conversation, IConversationService>();


            return services;
        }

    }
}
