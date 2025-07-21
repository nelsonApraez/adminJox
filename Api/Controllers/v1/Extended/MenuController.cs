namespace Api.Controllers
{
    using MediatR;

    /// <summary>
    /// Extended API to expose the methods of the entity [Menu]
    /// </summary>
    public partial class MenuController
    {
        /// <summary>
        /// Constructor to initialize the business rules and the context default instance for the entity [Menu]
        /// </summary>
        public MenuController(IMediator mediator) : base(mediator) { }
    }
}
