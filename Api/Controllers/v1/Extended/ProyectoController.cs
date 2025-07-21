namespace Api.Controllers.Controllers
{
    using MediatR;

    /// <summary>
    /// Api Extendida Para Exponer los Metodos de la Entidad [Proyecto]
    /// </summary>
    public partial class ProyectoController
    {
        /// <summary>
        /// Constructor para inicializar Reglas de Negocio e Instacia del Contexto Default para la entidad (Proyecto)
        /// </summary>
        public ProyectoController(IMediator mediator) :
              base(mediator)
        {
        }
    }
}
