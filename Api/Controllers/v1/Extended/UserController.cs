namespace Api.Controllers
{
    using MediatR;

    /// <summary>
    /// Extended API to expose the methods of the entity [User]
    /// </summary>
    public partial class UserController
    {
        /// <summary>
        /// Constructor to initialize the business rules and the context default instance for the entity [User]
        /// </summary>
        public UserController(IMediator mediator) : base(mediator) { }
    }
}
