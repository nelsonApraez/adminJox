namespace Api.Controllers.Controllers
{
    using MediatR;

    /// <summary>
    /// Api Extendida Para Exponer los Metodos de la Entidad [Processingresult]
    /// </summary>
    public partial class ProcessingresultController
    {
        /// <summary>
        /// Constructor para inicializar Reglas de Negocio e Instacia del Contexto Default para la entidad (Processingresult)
        /// </summary>
        public ProcessingresultController(IMediator mediator) :
              base(mediator)
        {
        }
    }
}
