namespace Api.Controllers
{
    using MediatR;

    /// <summary>
    /// Extended API to expose the methods of the entity [AuthorizationPermissions]
    /// </summary>
    public partial class AuthorizationPermissionsController
    {
        /// <summary>
        /// Constructor to initialize the business rules and the context default instance for the entity [AuthorizationPermissions]
        /// </summary>
        public AuthorizationPermissionsController(IMediator mediator) : base(mediator) { }
    }
}
