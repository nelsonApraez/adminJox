using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using JOX.Assistant.Interface;
using JOX.Assistant.Services;
using JOX.Assistant.Socket;
using JOX.Assistant.Storage;


namespace JOX.Assistant
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddJOXDependencyInjection(this IServiceCollection services, IConfiguration configuration)
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

            services.AddSingleton<IJOXStorage, JOXStorage>();

            services.AddTransient<IJOXService, JOXService>();

            services.AddSingleton<ICatalogJOX, CatalogJOXServices>();

            //services.AddSingleton<ChatHubJOX>();
            

            return services;
        }
    }
}
