using System;
using System.Threading.Tasks;
using Domain.Common;
using Infrastructure.Common;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using Package.Utilities.Net;
using Microsoft.Azure.Cosmos.Linq;
using System.Linq.Expressions;
using System.Linq;
using System.Collections.Generic;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;


namespace Infrastructure.Cosmos.Common
{
    public abstract partial class RepositoryBaseDao<T> : BaseRepositoryDao<T>, IRepositoryBase<T>
        where T : class, Domain.Common.Interfaces.IEntities, new()
    {
        private readonly Container _client;
        private readonly QueryRequestOptions _queryOptions;

        protected RepositoryBaseDao(Domain.Common.IMainContext contexto)
        {
            RepositoryContext = (Domain.Common.IMainContext)contexto;
            _client = ((Infrastructure.Cosmos.Common.MainContext)contexto).GetClient<T>();
            _queryOptions = new QueryRequestOptions()
            {
                MaxItemCount = 1000,                
                PartitionKey = new PartitionKey("Id")

            };
        }

        public long Count(Expression<Func<T, bool>> expression)
        {
            var query = _client
                  .GetItemLinqQueryable<T>(requestOptions: _queryOptions)
                  .Where(expression);
            return query != null ? query.Count():0;
        }

        public async Task<int?> CreateAsync(T objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {
                await this._client.CreateItemAsync<T>(objCreate, new PartitionKey("Id")).ConfigureAwait(false);
                returnCreate = 1;
            }

            return returnCreate;
        }

        public async Task<int?> CreateAsync(IEnumerable<T> objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {
                foreach (var item in objCreate)
                {
                    await CreateAsync(item);
                    returnCreate++;
                }
            }
            return returnCreate;
        }

        public async Task<bool?> DeleteAsync(T objDelete)
        {
            bool? returnDelete = null;
            if (objDelete.IsNotNull())
            {
                var result = await _client.DeleteItemAsync<T>(objDelete.Id,new PartitionKey("Id"));
                return result != null;
            }
            return returnDelete;
        }

        public async Task<bool?> DeleteRangeAsync(IEnumerable<T> objDelete)
        {
            bool? returnDelete = false;

            if (objDelete.IsNotNull())
            {
                foreach (var item in objDelete)
                {
                    returnDelete = await DeleteAsync(item);
                }
            }

            return returnDelete;
        }

        public async Task<bool?> EditAsync(T objEdit)
        {
            bool? returnEdit = null;
            if (objEdit.IsNotNull())
            {
                var edit = await _client.ReplaceItemAsync<T>(objEdit,objEdit.Id);
                return edit != null;
            }

            return returnEdit;
        }

        public async Task<bool?> EditAsync(IEnumerable<T> objEdit)
        {
            bool? returnEdit = null;
            if (objEdit.IsNotNull())
            {
                int resultado = 0;
                foreach (var item in objEdit)
                {
                    await EditAsync(item);
                    resultado++;
                }

                returnEdit = resultado != 0;
            }

            return returnEdit;
        }

        public async Task<bool> ExistElementAsync(Expression<Func<T, bool>> expression)
        {
            var query = _client
                .GetItemLinqQueryable<T>(requestOptions: _queryOptions)                
                .Where(expression);
            return await Task.FromResult(query!=null && query.Count() > 0);            
        }

        public T Search(Expression<Func<T, bool>> expression)
        {
            var query = _client
                .GetItemLinqQueryable<T>(requestOptions: _queryOptions)
                .Where(expression);
            return query?.FirstOrDefault();
        }

        public T Search(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return SearchAsync(expression).GetAwaiter().GetResult();
        }

        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression)
        {
            var query = _client
               .GetItemLinqQueryable<T>(requestOptions: _queryOptions)
               .Where(expression);
            return query?.ToList()?.FirstOrDefault();
        }

        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return await SearchAsync(expression);
        }

        public async Task<List<T>> SearchListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            var query = _client
              .GetItemLinqQueryable<T>(requestOptions: _queryOptions)
              .Where(expression);
            return await Task.FromResult(query?.ToList());
        }

        public async Task<CustomList<T>> ToListPaged()
        {
            return await ToListPaged(null);
        }

        public async Task<List<T>> ToListAsync()
        {
            var list = _client.GetItemLinqQueryable<T>(true).ToFeedIterator();
            var results = await list.ReadNextAsync();
            return results.ToList();
        }

        public async Task<List<T>> ToListAsync(ParameterOfList<T> parameterOfList)
        {
            IQueryable<T> lstBase = this._client.GetItemLinqQueryable<T>().AsQueryable();
            lstBase = ConfigureFilter(lstBase, parameterOfList);            
            lstBase = ConfigurePaged(lstBase, parameterOfList);
            var list = lstBase.ToFeedIterator();
            var results = await list.ReadNextAsync();
            return results.ToList();
        }
                

        public async Task<CustomList<T>> ToListPaged(ParameterOfList<T> parameterOfList)
        {
            IQueryable<T> lstBase = this._client.GetItemLinqQueryable<T>().AsQueryable();
            lstBase = ConfigureFilter(lstBase, parameterOfList);            
            lstBase = ConfigurePaged(lstBase, parameterOfList);
            //lstBase = ConfigureMaxCount(lstBase, parameterOfList);
            var list = lstBase.ToFeedIterator();
            var results = await list.ReadNextAsync();
            var listR = results.ToList();
            var oToList = new CustomList<T>(listR);
            if (parameterOfList.IsNotNull())
            {
                oToList.Paged = parameterOfList.TextPag;
                oToList.Paged.MaxCount = 300;
            }
            return await Task.FromResult(oToList);
        }
    }
}
