namespace Infrastructure.Repositories
{
    using Domain.Common;
    using Package.Utilities.Net;

    /// <summary>
    /// Class represents data access for Entity (Entity)
    /// </summary>
    [BusinessDaoAttribute]
    public partial class EntityRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.Entity>,
        Domain.Repositories.Interfaces.IEntityRepository
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (Entity)
        /// </summary>
        /// <param name="context">Database context instance</param>
        public EntityRepository(IMainContext context) : base(context) { } 
    }
}
