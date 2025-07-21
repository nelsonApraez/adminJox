
using Domain.Repositories.Interfaces;
using Domain.Services.Interfaces;
using Infrastructure.Repositories;
using Infrastructure.Service.OpenIA;
using Microsoft.Extensions.DependencyInjection;


namespace Infrastructure
{
    public static partial class DependencyInjection
    {

        public static IServiceCollection AddInfrastructureDenpendencyInjectionApp(this IServiceCollection services)
        {

            //Repositorios                                    

            services.AddScoped<IProyectoRepository, ProyectoRepository>();
            services.AddScoped<IPreguntaRepository, PreguntaRepository>();
            services.AddScoped<IRespuestaRepository, RespuestaRepository>();

            services.AddScoped<IPromptRepository, PromptRepository>();
            services.AddScoped<IProcessingresultRepository, ProcessingresultRepository>();

            services.AddScoped<IragIAServices, RagIAService>();
            services.AddScoped<IConversationRepository, ConversationRepository>();

            return services;
        }
    }
}


