using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.Extensions.Configuration;
using MongoDB.Driver;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Numeric;

namespace Infrastructure.Cosmos.Common
{
    public class MainContext : IMainContext
    {        
        private readonly string _dataBaseName = "DataBaseName";
        private readonly CosmosClient _cosmosClient;

        public MainContext(IConfiguration configuration)
        {
            _cosmosClient = new CosmosClient(configuration["CosmosDb:EndPoint"], configuration["CosmosDb:Secret"]);
            _dataBaseName = configuration["CosmosDb:DatabaseName"];
        }

        public Container GetClient<T>()
        {            
            return _cosmosClient.GetContainer(_dataBaseName,  typeof(T).Name.ToLower());
        }
        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new NotImplementedException();
        }

        public int ExecuteQuery(string sQuery)
        {
            throw new NotImplementedException();
        }

        public Task<int> ExecuteQueryAsync(string sQuery)
        {
            throw new NotImplementedException();
        }

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(0);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new NotImplementedException();
        }

       
    }
}
