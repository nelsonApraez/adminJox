using Domain.Common;
using Package.Utilities.Net;

namespace Infrastructure.Repositories
{


    /// <summary>
    /// Class represents data access for Entity (Role)
    /// </summary>
    [BusinessDaoAttribute]
    public partial class RoleRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.Role>,
        Domain.Repositories.Interfaces.IRoleRepository
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (Role)
        /// </summary>
        /// <param name="contexto">Database context instance</param>
        public RoleRepository(IMainContext context) : base(context) { } 
    }
}
