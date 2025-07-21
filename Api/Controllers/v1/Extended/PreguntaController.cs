namespace Api.Controllers.Controllers
{
    using MediatR;

    /// <summary>
    /// Api Extendida Para Exponer los Metodos de la Entidad [Pregunta]
    /// </summary>
    public partial class PreguntaController
    {
        /// <summary>
        /// Constructor para inicializar Reglas de Negocio e Instacia del Contexto Default para la entidad (Pregunta)
        /// </summary>
        public PreguntaController(IMediator mediator) :
              base(mediator)
        {
        }
    }
}
