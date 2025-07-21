using Domain.Common;
using Package.Utilities.Net;

namespace Infrastructure.Repositories
{


    /// <summary>
    /// Class represents data access for Entity (AuthorizationPermissions)
    /// </summary>
    [BusinessDaoAttribute]
    public partial class AuthorizationPermissionsRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.AuthorizationPermissions>,
        Domain.Repositories.Interfaces.IAuthorizationPermissionsRepository
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (AuthorizationPermissions)
        /// </summary>
        /// <param name="context">Database context instance</param>
        public AuthorizationPermissionsRepository(IMainContext context) : base(context) { } 
    }
}
