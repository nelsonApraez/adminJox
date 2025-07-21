namespace Api.Controllers
{
    using MediatR;

    /// <summary>
    /// Extended API to expose the methods of the entity [Entity]
    /// </summary>
    public partial class EntityController
    {
        /// <summary>
        /// Constructor to initialize the business rules and the context default instance for the entity [Entity]
        /// </summary>
        public EntityController(IMediator mediator) : base(mediator) { }
    }
}
