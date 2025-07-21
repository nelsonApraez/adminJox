namespace Api.Controllers.Controllers
{
    using MediatR;

    /// <summary>
    /// Api Extendida Para Exponer los Metodos de la Entidad [Prompt]
    /// </summary>
    public partial class PromptController
    {
        /// <summary>
        /// Constructor para inicializar Reglas de Negocio e Instacia del Contexto Default para la entidad (Prompt)
        /// </summary>
        public PromptController(IMediator mediator) :
              base(mediator)
        {
        }
    }
}
