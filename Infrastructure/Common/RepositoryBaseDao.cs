namespace Infrastructure.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Domain.Common;
    using Microsoft.EntityFrameworkCore;
    using Package.Utilities.Net;

    /// <summary>
    /// Clase base que permitira el acceso a datos para todas las entidades de negocio.
    /// </summary>
    /// <typeparam name="T">Entidad de Negocio</typeparam>
    public abstract class RepositoryBaseDao<T> : BaseRepositoryDao<T>, IRepositoryBase<T>
        // where T : Entity
        where T : class, new()
    {
        /// <summary>
        /// Metodo Contructor de la Clase Base
        /// </summary>
        /// <param name="contexto">Contexto de la entidad que contiene la instancia de la base de datos del proyecto</param>
        protected RepositoryBaseDao(IMainContext contexto)
        {
            RepositoryContext = contexto;
        }


        /// <summary>
        /// Objeto de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public T Search(Expression<Func<T, bool>> expression)
        {
            return Search(expression, null);
        }

        /// <summary>
        /// Objeto de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <param name="includes">Incluciones de relaciones sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public T Search(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            T objSearch = null;
            if (expression.IsNotNull())
            {
                objSearch = ConfigureIncludeSearch(GetInstanceRepository.Set<T>().AsNoTracking(), expression, includes).FirstOrDefault();
                EntityStateDetached(objSearch);
            }

            return objSearch;
        }



        /// <summary>
        /// Numero de Registros de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el numero de registros segun el filtro de la entidad <typeparamref name="T"/></returns>
        public long Count(Expression<Func<T, bool>> expression)
        {
            long returnCount = 0;
            if (expression.IsNotNull())
            {
                returnCount = ConfigureIncludeSearch(GetInstanceRepository.Set<T>().AsNoTracking(), expression).LongCount();
            }

            return returnCount;
        }

        /// <summary>
        /// Implementación Listado Async de Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <returns>Retorna el Listado segun los filtros de la entidad <typeparamref name="T"/></returns>
        public async Task<List<T>> ToListAsync()
        {
            return await ToListAsync(null);
        }

        /// <summary>
        /// Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="parameterOfList">Objeto para parametrizar el listado</param>
        /// <returns>Retorna Listado segun los filtros de la entidad y paginado definido<typeparamref name="T"/></returns>
        public async Task<List<T>> ToListAsync(ParameterOfList<T> parameterOfList)
        {
            return await ConfigureParameterOfList(GetInstanceRepository.Set<T>(), parameterOfList).ToListAsync();
        }

        /// <summary>
        /// Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression)
        {
            return await SearchAsync(expression, null);
        }

        /// <summary>
        /// Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <param name="includes">Incluciones de relaciones sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public async Task<T> SearchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            T objSearch = null;
            if (expression.IsNotNull())
            {
                objSearch = await ConfigureIncludeSearch(GetInstanceRepository.Set<T>(), expression, includes).FirstOrDefaultAsync();
            }
            if (objSearch != null && !GetInstanceRepository.GetType().FullName.Contains("Proxies"))
                GetInstanceRepository.Entry(objSearch).State = EntityState.Detached;
            return objSearch;
        }

        /// <summary>
        /// Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <param name="includes">Incluciones de relaciones sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public async Task<List<T>> SearchListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            List<T> objSearch = null;
            if (expression.IsNotNull())
            {
                objSearch = await ConfigureIncludeSearch(GetInstanceRepository.Set<T>(), expression, includes).ToListAsync().ConfigureAwait(false);
            }
            return objSearch;
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
                await GetInstanceRepository.Set<T>().AddAsync(objCreate);
                returnCreate = await RepositoryContextSaveChangesAsync().ConfigureAwait(false);
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
                await GetInstanceRepository.Set<T>().AddRangeAsync(objCreate);
                returnCreate = await RepositoryContextSaveChangesAsync().ConfigureAwait(false);
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
                GetInstanceRepository.Set<T>().Update(objEdit);
                returnEdit = await ReturnRepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnEdit;
        }

        /// <summary>
        /// Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="objEdit">Lista Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public async Task<bool?> EditAsync(IEnumerable<T> objEdit)
        {
            bool? returnEdit = null;
            if (objEdit.IsNotNull())
            {
                GetInstanceRepository.Set<T>().UpdateRange(objEdit);
                returnEdit = await ReturnRepositoryContextSaveChangesAsync().ConfigureAwait(false);
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
                GetInstanceRepository.Set<T>().Remove(objDelete);
                returnDelete = await ReturnRepositoryContextSaveChangesAsync().ConfigureAwait(false);
            }

            return returnDelete;
        }

        /// <summary>
        /// Eliminación masiva Async del Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="objDelete">Lista de Lista de Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el o los registros)</returns>
        public async Task<bool?> DeleteRangeAsync(IEnumerable<T> objDelete)
        {
            bool? returnDelete = null;
            if (objDelete.IsNotNull())
            {
                GetInstanceRepository.Set<T>().RemoveRange(objDelete);
                returnDelete = await ReturnRepositoryContextSaveChangesAsync().ConfigureAwait(false);
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
        /// Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="parameterOfList">Objeto para parametrizar el listado</param>
        /// <returns>Retorna Listado y objeto compusto de paginacion segun los filtros de la entidad y paginado definido<typeparamref name="T"/></returns>
        public async Task<CustomList<T>> ToListPaged(ParameterOfList<T> parameterOfList)
        {
            var oToList = new CustomList<T>(ConfigureParameterOfList(GetInstanceRepository.Set<T>(), parameterOfList));
            if (parameterOfList.IsNotNull())
            {
                oToList.Paged = parameterOfList.TextPag;
            }

            return await Task.FromResult(oToList);
        }

        /// <summary>
        /// Implementacion para identificar si existe un elemento
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        public async Task<bool> ExistElementAsync(Expression<Func<T, bool>> expression)
        {
            var result = await SearchAsync(expression);
            return !result.IsNotNull();
        }
    }
}
