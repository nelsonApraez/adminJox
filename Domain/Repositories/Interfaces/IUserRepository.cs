namespace Domain.Repositories.Interfaces
{
    using System.Threading.Tasks;
    using Domain.Common;


    /// <summary>
    /// Interface represents the Implementations of the Dao for the Entity (User)
    /// </summary>
    public partial interface IUserRepository :
        IRepositoryBase<Domain.AggregateModels.User> {

        public abstract Task<Domain.AggregateModels.User> LoginAsync(Domain.AggregateModels.User user);
    }
}
