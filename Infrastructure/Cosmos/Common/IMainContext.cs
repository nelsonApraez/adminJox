using Microsoft.Azure.Cosmos;
using MongoDB.Driver;

namespace Infrastructure.Cosmos.Common
{
    public partial interface IMainContext : Domain.Common.IMainContext
    {
        public Container GetClient<T>();
    }
}
