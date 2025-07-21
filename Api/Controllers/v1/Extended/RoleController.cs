namespace Api.Controllers
{
    using MediatR;

    /// <summary>
    /// Extended API to expose the methods of the entity [Role]
    /// </summary>
    public partial class RoleController
    {
        /// <summary>
        /// Constructor to initialize the business rules and the context default instance for the entity [Role]
        /// </summary>
        public RoleController(IMediator mediator) : base(mediator) { }
    }
}
