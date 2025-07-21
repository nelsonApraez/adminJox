using System.Reflection;
using Microsoft.OpenApi.Models;
using Package.Utilities.Net;

namespace Api.Installers
{
    public class Swagger : IInstaller
    {
        protected readonly SwaggerConfiguration _swaggerConfiguration = new();

        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            configuration.Bind(Constants.SwaggerConfiguration, _swaggerConfiguration);

            services.AddSwaggerGen(swagger =>
            {
                swagger.DescribeAllParametersInCamelCase();

                swagger.AddSecurityDefinition(Constants.Bearer, new OpenApiSecurityScheme
                {
                    Description = Constants.ExampleDescription,
                    Name = Constants.Autorization,
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = Constants.Bearer
                });



                swagger.AddSecurityRequirement(new OpenApiSecurityRequirement
                  {
                    {
                      new OpenApiSecurityScheme
                      {
                        Reference = new OpenApiReference
                          {
                            Type = ReferenceType.SecurityScheme,
                            Id = Constants.Bearer
                          },
                          Scheme = Constants.Oauth2,
                          Name = Constants.Bearer,
                          In = ParameterLocation.Header,
                        },
                        new List<string>()
                      }
                    });

                swagger.SwaggerDoc(_swaggerConfiguration.DocInfoVersion, GetApiInfo);

                swagger.DocInclusionPredicate((docName, description) => true);
                swagger.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);

                swagger.IncludeXmlComments(xmlPath);

            });

        }

        /// <summary>
        /// Obtener Objeto de Contacto para la api de Swagger
        /// </summary>
        /// <returns>Objeto OpenApiContact</returns>
        protected OpenApiContact GetApiContact => new()
        {
            Name = _swaggerConfiguration.ContactName,
            Url = _swaggerConfiguration.ContactUrl,
            Email = _swaggerConfiguration.ContactEmail
        };


        /// <summary>
        /// Obtener Objeto de la Información para la api de Swagger
        /// </summary>
        /// <returns>Objeto OpenApiInfo</returns>
        protected OpenApiInfo GetApiInfo => new()
        {
            Title = _swaggerConfiguration.DocInfoTitle,
            Version = _swaggerConfiguration.DocInfoVersion,
            Description = _swaggerConfiguration.DocInfoDescription + "<br><b>" + System.Environment.MachineName + "</b>",
            Contact = GetApiContact
        };
    }
}
