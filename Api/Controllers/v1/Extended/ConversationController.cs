namespace Api.Controllers.Controllers
{
    using MediatR;

    /// <summary>
    /// Api Extendida Para Exponer los Metodos de la Entidad [Conversation]
    /// </summary>
    public partial class ConversationController
    {
        /// <summary>
        /// Constructor para inicializar Reglas de Negocio e Instacia del Contexto Default para la entidad (Conversation)
        /// </summary>
        public ConversationController(IMediator mediator) :
              base(mediator)
        {
        }
    }
}
