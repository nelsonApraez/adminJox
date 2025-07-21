using Domain.Common;
using Package.Utilities.Net;

namespace Infrastructure.Repositories
{
  

    /// <summary>
    /// Class represents data access for Entity (Menu)
    /// </summary>
    [BusinessDaoAttribute]
    public partial class MenuRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.Menu>,
        Domain.Repositories.Interfaces.IMenuRepository
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (Menu)
        /// </summary>
        /// <param name="context">Database context instance</param>
        public MenuRepository(IMainContext context) : base(context) { } 
    }
}
