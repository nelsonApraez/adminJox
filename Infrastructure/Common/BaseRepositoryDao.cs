namespace Infrastructure.Common
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Domain.Common;
    using Microsoft.EntityFrameworkCore;
    using Package.Utilities.Net;

    /// <summary>
    /// Clase base que permitira centralizar las configuraciones de acceso a datos para todas las entidades de negocio
    /// </summary>
    /// <typeparam name="T">Entidad de Negocio</typeparam>
    public abstract partial class BaseRepositoryDao<T>
          where T : class, new()
    {
        /// <summary>
        /// Guardar automaticamente en la la Persistencia (BD...)
        /// </summary>
        protected bool AutoSave = true;

        /// <summary>
        /// Entidad que contiene el contexto de base de datos del proyecto
        /// </summary>
        public IMainContext RepositoryContext { get; protected set; }

        /// <summary>
        /// Contexto de base de datos del proyecto
        /// </summary>
        protected IMainContext GetInstanceRepository => RepositoryContext;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iQuery"></param>
        /// <param name="expression"></param>
        /// <param name="includes"></param>
        /// <returns></returns>
        protected IQueryable<T> ConfigureIncludeSearch(IQueryable<T> iQuery, Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            if (includes.IsNotNull())
            {
                iQuery = includes.Aggregate(iQuery,
                                (current, include) => current.Include(include));
            }

            iQuery = iQuery.Where(expression);

            return iQuery;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="iQuery"></param>
        /// <param name="parameterOfList"></param>
        /// <returns></returns>
        protected IQueryable<T> ConfigureInclude(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.Include.IsNotNull())
            {
                iQuery = parameterOfList.Include.Aggregate(iQuery,
                                (current, include) => current.Include(include));
            }

            return iQuery;
        }

        /// <summary>
        /// Setear valor para guardar automaticamente en la la Persistencia (BD...)
        /// </summary>
        public void SetAutoSave(bool value)
        {
            AutoSave = value;
        }

        /// <summary>
        /// Metodo para Liberar el objeto obtenido.
        /// </summary>
        /// <param name="obj">Entidad especificada <typeparamref name="T"/></param>
        protected void EntityStateDetached(T obj)
        {
            if (obj.IsNotNull() && GetInstanceRepository.Entry(obj).IsNotNull())
            {
                GetInstanceRepository.Entry(obj).State = EntityState.Detached;
            }
        }

        /// <summary>
        /// Metodo para ejecutar sobre la base de datos y valida si hay alteraciones
        /// </summary>
        /// <returns>Retorna tru si hay alteraciones o false si no se altero ningun registro</returns>
        protected async Task<bool> ReturnRepositoryContextSaveChangesAsync()
        {
            return AutoSave && (await RepositoryContextSaveChangesAsync().ConfigureAwait(false) != 0);
        }

        /// <summary>
        /// Metodo para ejecutar sobre la base de datos
        /// </summary>
        /// <returns>Retorna nuemero de registros alterados</returns>
        protected async Task<int> RepositoryContextSaveChangesAsync()
        {
            return AutoSave ? (await GetInstanceRepository.SaveChangesAsync().ConfigureAwait(false)) : 0;
        }
    }
}
