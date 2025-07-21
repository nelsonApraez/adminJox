using MediatR;

namespace Api.Controllers
{
    using Package.Utilities.Net;

    /// <summary>
    /// Clase base de utilidades de los controllers de la api
    /// </summary>
    public abstract partial class BaseUtilities
    {
        /// <summary>
        /// Componente de implementacion de patron Mediator
        /// </summary>
        protected IMediator _mediator;


        /// <summary>
        /// Time zone de la solicitud
        /// </summary>
        protected int TimeZone { get => (HttpContext != null ? Convert.ToInt32(HttpContext.Request.Headers.ContainsKey("X-Timezone-Offset") ? HttpContext.Request.Headers["X-Timezone-Offset"][0] : '0') : 0); }

        /// <summary>
        /// Codigo CusMessage De Mensaje de Creación "CusMessageCreate"
        /// </summary>
        protected EnumerationException.Message CusMessageCreate = EnumerationException.Message.MsjCreacion;
        /// <summary>
        /// Codigo CusMessage De Mensaje de Actualización "CusMessageUpdate"
        /// </summary>
        protected EnumerationException.Message CusMessageUpdate = EnumerationException.Message.MsjActualizacion;
        /// <summary>
        /// Codigo CusMessage De Mensaje de Eliminación "CusMessageDelete"
        /// </summary>
        protected EnumerationException.Message CusMessageDelete = EnumerationException.Message.MsjEliminacion;

        protected BaseUtilities(IMediator mediator)
        {
            _mediator = mediator;
        }


        /// <summary>
        /// Nombre de la Clase Referenciada trabajada
        /// </summary>
        protected string NameClassReference { get; set; }
    }
}
