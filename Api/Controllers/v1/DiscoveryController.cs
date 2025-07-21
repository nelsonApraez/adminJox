namespace Api.Controllers.Controllers
{
    using System.Collections.Generic;
    using System.Linq;
    using Package.Utilities.Net;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Controllers;
    using Microsoft.AspNetCore.Routing;

    /// <summary>
    /// Api Para Exponer los Metodos de la Entidad [Descubrimiento]
    /// </summary>
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public partial class DiscoveryController : ControllerBase
    {
        protected readonly SwaggerConfiguration SwaggerConfiguration;
        private readonly IEnumerable<EndpointDataSource> EndpointSources;

        public DiscoveryController(IEnumerable<EndpointDataSource> endpointSources, SwaggerConfiguration swaggerConfiguration)
        {
            EndpointSources = endpointSources;
            SwaggerConfiguration = swaggerConfiguration;
        }

        /// <summary>
        /// Retorna la información general del servicio
        /// </summary>
        /// <returns>
        /// Retorna un response HTTP con StatusCodes
        ///         200OK Retorna Un Objeto Compuesto, con la información de descubrimiento del servicio
        ///         400BadRequest Sí ocurrió una falla, validación o error controlado
        ///         500InternalServerError Sí ocurrió una falla o error NO controlado
        ///         403Forbidden Sí no tiene permisos para ejecutar la acción
        ///         401Unauthorized Sí no esta autenticado
        /// </returns>
        /// <response code="200">Retorna Un Objeto Compuesto, con la información de descubrimiento del servicio</response>
        /// <response code="400">Sí ocurrió una falla, validación o error controlado</response>
        /// <response code="500">Sí ocurrió una falla o error NO controlado</response>
        /// <response code="403">Sí no tiene permisos para ejecutar la acción</response>
        /// <response code="401">Sí no esta autenticado</response>
        [HttpGet("")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Application.Features.Models.Dto.DescubrimientoDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status403Forbidden, Type = typeof(ResponseApi))]
        [ProducesResponseType(StatusCodes.Status401Unauthorized)]
        public IActionResult GetDiscoveryAsync() { return new OkObjectResult(GetCustomParameters); }

        protected IDictionary<string, object> GetCustomParameters => new Dictionary<string, object>
        {
            { Constants.DiscoveryTag.Aplicacion, SwaggerConfiguration.App},
            { Constants.DiscoveryTag.Modulo, SwaggerConfiguration.Module},
            { Constants.DiscoveryTag.Funcionalidad, SwaggerConfiguration.Functionality},
            { Constants.DiscoveryTag.Analistaresponsable, SwaggerConfiguration.ResponsibleAnalyst},
            { Constants.DiscoveryTag.Eemailanalistaresponsable, SwaggerConfiguration.EmailResponsibleAnalyst},
            { Constants.DiscoveryTag.Endpointgobierno, new Dictionary<string, string>
               {
                { Constants.DiscoveryTag.Swagger, SwaggerConfiguration.EndpointSwagger},
                { Constants.DiscoveryTag.Health, SwaggerConfiguration.EndpointHealth},
                { Constants.DiscoveryTag.Discovery, SwaggerConfiguration.EndpointDiscovery}
               }
            },
            { Constants.DiscoveryTag.Version, new Dictionary<string, string>
               {
                { Constants.DiscoveryTag.Pordefecto, SwaggerConfiguration.DocInfoVersion},
                { Constants.DiscoveryTag.Ultima, SwaggerConfiguration.DocInfoVersionLatest},
                { Constants.DiscoveryTag.V1, Constants.DiscoveryTag.V1},
                { Constants.DiscoveryTag.V2, Constants.DiscoveryTag.V2},
               }
            },
            { Constants.DiscoveryTag.Endpointsaplicacion,  ListAllEndpoints()}
        };

        private object ListAllEndpoints()
        {
            var endpoints = EndpointSources
                .SelectMany(es => es.Endpoints)
                .OfType<RouteEndpoint>();
            var output = endpoints.Select(
                e =>
                {
                    var controller = e.Metadata
                        .OfType<ControllerActionDescriptor>()
                        .FirstOrDefault();
                    var action = controller != null
                        ? $"{controller.ControllerName}.{controller.ActionName}"
                        : null;
                    return new
                    {
                        Metodo = e.Metadata.OfType<HttpMethodMetadata>().FirstOrDefault()?.HttpMethods?[0],
                        Ruta = $"/{e.RoutePattern.RawText.TrimStart('/')}",
                        Accion = action
                    };
                }
            );

            return output;
        }
    }
}
