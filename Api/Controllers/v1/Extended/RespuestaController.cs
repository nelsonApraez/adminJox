namespace Api.Controllers.Controllers
{
    using MediatR;

    /// <summary>
    /// Api Extendida Para Exponer los Metodos de la Entidad [Respuesta]
    /// </summary>
    public partial class RespuestaController
    {
        /// <summary>
        /// Constructor para inicializar Reglas de Negocio e Instacia del Contexto Default para la entidad (Respuesta)
        /// </summary>
        public RespuestaController(IMediator mediator) :
              base(mediator)
        {
        }
    }
}
