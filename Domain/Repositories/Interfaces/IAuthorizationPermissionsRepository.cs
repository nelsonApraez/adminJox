using Domain.Common;

namespace Domain.Repositories.Interfaces
{
    

    /// <summary>
    /// Interface represents the Implementations of the Dao for the Entity (AuthorizationPermissions)
    /// </summary>
    public partial interface IAuthorizationPermissionsRepository :
        IRepositoryBase<Domain.AggregateModels.AuthorizationPermissions> { }
}
