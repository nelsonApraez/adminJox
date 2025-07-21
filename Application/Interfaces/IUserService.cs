using System.Threading.Tasks;
using Domain.AggregateModels;

namespace Application.Features.Interfaces
{
    /// <summary>
    /// This Interface represents the Business Implementations for the Entity (User)
    /// </summary>
    public partial interface IUserService :
        BaseApplicationHelper.IBaseApplicationHelper<Domain.AggregateModels.User> {

        public abstract Task<User> LoginAsync(User user);
    }
}
