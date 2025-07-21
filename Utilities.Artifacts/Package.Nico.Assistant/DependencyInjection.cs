using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Nico.Assistant.Interface;
using Nico.Assistant.Services;
using Nico.Assistant.Socket;
using Nico.Assistant.Storage;


namespace Nico.Assistant
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddNicoDenpendencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<ICosmosDbService, CosmosDbService>(sp =>
            {
                var repository = new CosmosDbService(configuration);
                return repository;
            });

            services.AddSingleton<IRulesEngineServices>(sp =>
            {
                var rulesEngineServices = new RulesEngineServices(configuration);
                return rulesEngineServices;
            });

            services.AddSingleton<IIARagService>(sp =>
            {
                var iARagService = new IARagService(configuration);
                return iARagService;
            });

            services.AddSingleton<IOpenIAServices>(sp =>
            {
                var openIAServices = new OpenIAServices(configuration);
                return openIAServices;
            });

            services.AddSingleton<IAzureAIService>(services =>
            {
                var azureAIService = new AzureAIService(configuration);
                return azureAIService;
            });

            services.AddSingleton<INicoStorage, NicoStorage>();

            services.AddTransient<INicoService, NicoService>();

            services.AddSingleton<ICatalogNico, CatalogNicoServices>();

            //services.AddSingleton<ChatHubNico>();
            

            return services;
        }
    }
}
