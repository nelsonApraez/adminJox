using System;
using System.Linq;
using Domain.Common;
using Domain.Repositories.Interfaces;
using Infrastructure.Common;
using Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Package.Utilities.Net;


namespace Infrastructure
{
    public static partial class DependencyInjection
    {
        /// <summary>
        /// Crear los Db context Genericos.
        /// </summary>
        /// <typeparam name="TService">TServices.</typeparam>
        /// <typeparam name="TContext">TContext.</typeparam>
        /// <param name="services">Services.</param>
        /// <param name="conection">Cadena de conexión.</param>
        /// <param name="lazyLoading">Lazy Loading.</param>
        public static IServiceCollection AddInfrastructureDenpendencyInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<MainContext>(options =>
                    options
                    .UseSqlServer(GetConnectionKeyVault($"{EnumerationApplication.AplicationEnums.DefaultConnectionSqlServer}"),
                     option =>
                     option.EnableRetryOnFailure()
                    )
                    .UseLazyLoadingProxies(true)
                    , ServiceLifetime.Transient); ;

            services.AddTransient<IMainContext>(provider => provider.GetService<MainContext>());
            services.Configure<Infrastructure.Mongo.Common.MongoSettings>(option =>
            {
                option.ConnectionString = GetConnectionKeyVault($"{EnumerationApplication.AplicationEnums.DefaultConnectionMongo}");
                option.MongoDatabase = configuration.GetSection("ConnectionStrings:MongoDatabase").Value;
            });
            services.AddScoped<Infrastructure.Mongo.Common.IMainContext, Infrastructure.Mongo.Common.MainContext>();
            services.AddScoped<Infrastructure.Cosmos.Common.IMainContext, Infrastructure.Cosmos.Common.MainContext>();

            //Repositorios                                    
            //services.AddScoped<IProyectoRepository, ProyectoRepository>();
            //services.AddScoped<IPreguntaRepository, PreguntaRepository>();
            //services.AddScoped<IRespuestaRepository, RespuestaRepository>();

            services.AddScoped<IMonedaRepository, MonedaRepository>();
            services.AddScoped<IRoleRepository, RoleRepository>();
            services.AddScoped<IEntityRepository, EntityRepository>();
            services.AddScoped<IUserRepository, UserRepository>();
            services.AddScoped<IMenuRepository, MenuRepository>();
            services.AddScoped<IAuthorizationPermissionsRepository, AuthorizationPermissionsRepository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(DigiToolsMessage.TextMessageApplication.TextMessageHandler).Assembly));
                       

            services.AddInfrastructureDenpendencyInjectionApp();

            services.AddScoped<Domain.Services.ICatalogoMensajeService, DigiToolsMessage.TextMessageApplication.CatalogoMensajeService>();

            var servicesDomain = AppDomain.CurrentDomain.GetAssemblies()
                 .Where(assembly => (assembly?.FullName?.Contains("Domain", StringComparison.InvariantCulture)) ?? false)
                     .SelectMany(s => s.GetTypes())
                         .Where(p => p.CustomAttributes.Any(x => x.AttributeType == typeof(DomainServiceAttribute)));

            foreach (var service in servicesDomain)
                services.AddTransient(service);
 

            services.AddHealthChecks().AddApplicationInsightsPublisher(configuration[$"ApplicationInsights:{EnumerationApplication.AplicationEnums.InstrumentationKey}"]);

            services.AddHealthChecks().AddSqlServer(GetConnectionKeyVault($"{EnumerationApplication.AplicationEnums.DefaultConnectionSqlServer}"));



            /// <summary>
            /// Se encarga de obtener la conexión.
            /// </summary>
            /// <param name="ValueKey"></param>
            /// <returns></returns>
            string GetConnectionKeyVault(string valueKey)
            {

                var connection = configuration[$"secret:{valueKey}"];
                if (string.IsNullOrEmpty(connection))
                {
                    connection = configuration.GetSection($"ConnectionStrings:{valueKey}").Value;
                }
                return connection;
            }
            return services;
        }


    }
}
