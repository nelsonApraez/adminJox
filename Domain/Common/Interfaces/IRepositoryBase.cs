namespace Domain.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Threading.Tasks;
    using Package.Utilities.Net;

    /// <summary>
    /// Interfaz Base que permitira definir todas las operaciones transversales para la persistencias de las entidades de negocio <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">Entidad de Negocio</typeparam>
    public interface IRepositoryBase<T>
        // where T : Entity
        where T : class, new()
    {
        /// <summary>
        /// Propiedad que contiene el contexto de base de datos
        /// </summary>
        IMainContext RepositoryContext { get; }

        /// <summary>
        /// Setear valor para guardar automaticamente en la la Persistencia (BD...)
        /// </summary>
        void SetAutoSave(bool value);

        /// <summary>
        /// Implementación Objeto de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        T Search(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Implementación Objeto de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <param name="includes">Incluciones de relaciones sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        T Search(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Numero de Registros de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el numero de registros segun el filtro de la entidad <typeparamref name="T"/></returns>
        long Count(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Implementación Listado Async de Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <returns>Retorna el Listado segun los filtros de la entidad <typeparamref name="T"/></returns>
        Task<List<T>> ToListAsync();

        /// <summary>
        /// Implementación Listado Async de Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el Listado segun los filtros de la entidad <typeparamref name="T"/></returns>
        Task<List<T>> ToListAsync(ParameterOfList<T> parameterOfList);

        /// <summary>
        /// Implementación Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        Task<T> SearchAsync(Expression<Func<T, bool>> expression);

        /// <summary>
        /// Objeto Async de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <param name="includes">Incluciones de relaciones sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        Task<T> SearchAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<List<T>> SearchListAsync(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes);

        /// <summary>
        /// Implementación Creación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        Task<int?> CreateAsync(T objCreate);

        /// <summary>
        /// Implementación Creación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Lista de Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        Task<int?> CreateAsync(IEnumerable<T> objCreate);

        /// <summary>
        /// Implementación Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        Task<bool?> EditAsync(T objEdit);

        /// <summary>
        /// Implementación Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        Task<bool?> EditAsync(IEnumerable<T> objEdit);

        /// <summary>
        /// Implementación Eliminación Async del Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="objDelete">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        Task<bool?> DeleteAsync(T objDelete);

        /// <summary>
        /// Implementación Eliminación masiva Async del Objetos de Negocio de la Entidad <typeparamref name="T"/> 
        /// </summary>
        /// <param name="objDelete">Lista Entidad especifica <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el o los registros)</returns>
        Task<bool?> DeleteRangeAsync(IEnumerable<T> objDelete);

        /// <summary>
        /// Implementación Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <returns>Retorna Objeto Compuesto Listado segun los filtros de la entidad y paginado definido<typeparamref name="T"/></returns>
        Task<CustomList<T>> ToListPaged();

        /// <summary>
        /// Implementación Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="parameterOfList">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna Objeto Compuesto Listado segun los filtros de la entidad y paginado definido<typeparamref name="T"/></returns>
        Task<CustomList<T>> ToListPaged(ParameterOfList<T> parameterOfList);


        /// <summary>
        /// Implementacion para identificar si existe un elemento
        /// </summary>
        /// <param name="expression"></param>
        /// <returns></returns>
        Task<bool> ExistElementAsync(Expression<Func<T, bool>> expression);
    }
}
