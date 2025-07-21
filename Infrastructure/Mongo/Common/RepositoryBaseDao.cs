namespace Infrastructure.Mongo.Common
{
    using Application.Common;
    using Domain.Common;    
    using Infrastructure.Common;
    using Microsoft.EntityFrameworkCore;
    using MongoDB.Driver;
    using Package.Utilities.Net;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;


    /// <summary>
    /// Clase base que permitira el acceso a datos para todas las entidades de negocio.
    /// </summary>
    /// <typeparam name="T">Entidad de Negocio</typeparam>
    public abstract partial class RepositoryBaseDao<T> : BaseRepositoryDao<T>, IRepositoryBase<T>
        where T : class, Domain.Common.Interfaces.IEntities, new()
    {
        /// <summary>
        /// Entidad que contiene el contexto de base de datos del proyecto
        /// </summary>
        protected readonly IMongoCollection<T> _dbCollection;

        /// <summary>
        /// Metodo Contructor de la Clase Base
        /// </summary>
        /// <param name="contexto">Contexto de la entidad que contiene la instancia de la base de datos del proyecto</param>
        protected RepositoryBaseDao(Domain.Common.IMainContext contexto)
        {
            RepositoryContext = (Domain.Common.IMainContext)contexto;
            _dbCollection = ((Infrastructure.Mongo.Common.IMainContext)RepositoryContext).GetCollection<T>();            
        }



        /// <summary>
        /// Implementación Listado Async de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <returns>Retorna el Listado segun los filtros de la entidad <typeparamref name="T"/></returns>
        public Task<List<T>> ToListAsync()
        {
            return ToListAsync(null);
        }

        /// <summary>
        /// Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="parameterOfList">Objeto para parametrizar el listado</param>
        /// <returns>Retorna Listado segun los filtros de la entidad y paginado definido<typeparamref name="T"/></returns>
        public async Task<List<T>> ToListAsync(ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList != null && (parameterOfList.Filter != null || parameterOfList.WhereDynamic.Filters != null))
            {
                IQueryable<T> lstBase = this._dbCollection.AsQueryable();
                return await ConfigureParameterOfList(lstBase, parameterOfList).ToListAsync();                
            }
            else
            {
                IAsyncCursor<T> lstBase = await this._dbCollection.FindAsync(Builders<T>.Filter.Empty).ConfigureAwait(false);
                return await lstBase.ToListAsync().ConfigureAwait(false);
            }            
           
        }

        /// <summary>
        /// Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression)
        {
            T obj = null;
            if (expression.IsNotNull())
            {
                obj = await (await this._dbCollection.FindAsync(expression).ConfigureAwait(false)).FirstOrDefaultAsync().ConfigureAwait(false);
            }

            return obj;
        }

        /// <summary>
        /// Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <param name="includes">Incluciones de relaciones sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public Task<T> SearchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return SearchAsync(expression);
        }

        /// <summary>
        /// Creación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        public async Task<int?> CreateAsync(T objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {                
                await this._dbCollection.InsertOneAsync(objCreate).ConfigureAwait(false);
                returnCreate = 1;
            }

            return returnCreate;
        }

        /// <summary>
        /// Creación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Lista de Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        public async Task<int?> CreateAsync(IEnumerable<T> objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {
                await this._dbCollection.InsertManyAsync(objCreate).ConfigureAwait(false);
                returnCreate = 1;
            }

            return returnCreate;
        }

        /// <summary>
        /// Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public async Task<bool?> EditAsync(T objEdit)
        {
            bool? returnEdit = null;
            if (objEdit.IsNotNull())
            {                
                returnEdit = (await this._dbCollection.ReplaceOneAsync(ObjectBaseExtensions.GetQueryFilterId<T>(objEdit), objEdit).ConfigureAwait(false)).IsAcknowledged;
                return returnEdit;
            }

            return returnEdit;
        }

        /// <summary>
        /// Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
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

        /// <summary>
        /// Eliminación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objDelete">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public async Task<bool?> DeleteAsync(T objDelete)
        {
            bool? returnDelete = null;
            if (objDelete.IsNotNull())
            {                
                returnDelete = (await this._dbCollection.DeleteOneAsync(ObjectBaseExtensions.GetQueryFilterId<T>(objDelete)).ConfigureAwait(false)).IsAcknowledged;
            }

            return returnDelete;
        }

        /// <summary>
        /// Eliminación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public async Task<bool?> DeleteAsync(Expression<Func<T, bool>> expression)
        {
            bool? returnDelete = null;
            if (expression.IsNotNull())
            {
                var objDelete = this._dbCollection.FindAsync(expression);
                if (objDelete.IsNotNull())
                {
                    returnDelete = (await this._dbCollection.DeleteManyAsync(expression).ConfigureAwait(false)).IsAcknowledged;
                }
                else
                {
                    returnDelete = false;
                }
            }

            return returnDelete;
        }

        /// <summary>
        /// Eliminación maxiva Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el o los registros)</returns>
        public async Task<bool?> DeleteRangeAsync(Expression<Func<T, bool>> expression)
        {
            bool? returnDelete = null;
            if (expression.IsNotNull())
            {
                var objDelete = this._dbCollection.FindAsync(expression);
                if (objDelete.IsNotNull())
                {
                    returnDelete = await DeleteAsync(expression);
                }
                else
                {
                    returnDelete = false;
                }
            }

            return returnDelete;
        }

        /// <summary>
        /// Eliminación maxiva Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objDelete">Objeto a eliminar</param>
        /// <returns>Retorna si la operacion fue exitosa (altero el o los registros)</returns>
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
        

        /// <summary>
        /// Implementación Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <returns>Retorna Objeto Compuesto Listado segun los filtros de la entidad y paginado definido<typeparamref name="T"/></returns>
        public async Task<CustomList<T>> ToListPaged()
        {
            return await ToListPaged(null);
        }

        /// <summary>
        /// Implementación Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="parameterOfList">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna Objeto Compuesto Listado segun los filtros de la entidad y paginado definido<typeparamref name="T"/></returns>
        public async Task<CustomList<T>> ToListPaged(ParameterOfList<T> parameterOfList)
        {
            IQueryable<T> lstBase = this._dbCollection.AsQueryable();
            lstBase = ConfigureFilter(lstBase, parameterOfList);
            lstBase = ConfigureMaxCount(lstBase, parameterOfList);
            lstBase = ConfigurePaged(lstBase, parameterOfList);
            var oToList = new CustomList<T>(lstBase);
            if (parameterOfList.IsNotNull())
            {
                oToList.Paged = parameterOfList.TextPag;
            }
            return await Task.FromResult(oToList);
        }

        /// <summary>
        /// Setear valor para guardar automaticamente en la la Persistencia (BD...)
        /// </summary>
        public new void SetAutoSave(bool value) {
            AutoSave = value;
        }

       public async Task<List<T>> SearchListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
       {
            var lstBase= await this._dbCollection.FindAsync(expression).ConfigureAwait(false);               
            return await lstBase.ToListAsync().ConfigureAwait(false);
        }

       
        
        public async Task<bool> ExistElementAsync(Expression<Func<T, bool>> expression)
        {
            var result = await SearchAsync(expression);
            return !result.IsNotNull();
        }
    }
}
