using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Domain.Common;
using Package.Utilities.Net;

namespace Application.BaseApplicationHelper
{
    public interface IBaseApplicationHelper<ENT> where ENT : class, new()
    {
        /// <summary>
        /// Implementación Creación Async del Objetos de Negocio de la Entidad <typeparamref name="ENT"/>
        /// </summary>
        /// <param name="objCreate">Entidad especificada <typeparamref name="ENT"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        Task<int?> CreateAsync(ENT objCreate);

        /// <summary>
        /// Implementación Eliminación Async del Objetos de Negocio de la Entidad <typeparamref name="ENT"/> 
        /// </summary>
        /// <param name="objDelete">Entidad especificada <typeparamref name="ENT"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        /// 
        Task<bool?> DeleteAsync(ENT objDelete);

        /// <summary>
        /// Implementación Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="ENT"/> 
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="ENT"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        Task<bool?> EditAsync(ENT objEdit);

        /// <summary>
        /// Obtiene el objeto por el ID
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        Task<ENT> GetByIdAsync(string id);

        /// <summary>
        /// Nombre de la Clase Referenciada trabajada
        /// </summary>
        string NameClassReference { get; }

        /// <summary>
        /// Propiedad que contiene el contexto de base de datos
        /// </summary>
        IRepositoryBase<ENT> Repository { get; }


        /// <summary>
        /// Validacion de invarianza
        /// </summary>
        /// <param name="objCurrent"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public Task<List<ResponseApi>> ValidateRules(ENT objCurrent, EnumerationApplication.Operations level);


        /// <summary>
        /// Cambia el estado de activacion de la entidad de negocio
        /// </summary>
        /// <param name="objEntity">Objeto que se va a cambiar de estado</param>
        /// <param name="propertyState">Nombre del campo estado que va a comparar del objeto</param>
        /// <returns></returns>
        public Task<List<ResponseApi>> ChangeStateEntity(ENT objEntity, string propertyState);



        /// <summary>
        /// Implementación Listado Async de Objetos de Negocio de la Entidad <typeparamref name="ENT"/> 
        /// </summary>
        /// <returns>Retorna el Listado segun los filtros de la entidad <typeparamref name="ENT"/></returns>
        Task<List<ENT>> ToListAsync();



        /// <summary>
        /// Implementación Listado de Objetos de Negocio de la Entidad <typeparamref name="ENT"/>
        /// </summary>
        /// <param name="parameterOfList">Filtros sobre la entidad <typeparamref name="ENT"/></param>
        /// <param name="objFilter">Filtros sobre la entidad <typeparamref name="ENT"/></param>
        /// <returns>Retorna Objeto Compuesto Listado segun los filtros de la entidad y paginado definido<typeparamref name="ENT"/></returns>
        Task<CustomList<ENT>> ToListPaged(ParameterGetList parameterGetList, Filter objFilter);


        /// <summary>
        /// Se encarga de obtener la data para exportar a Excel.
        /// </summary>
        /// <param name="objFilter">Contiene los filtros a consultar para exportar.</param>
        /// <returns>Data para exportar a Excel.</returns>
        Task<List<ENT>> GetDataAsync(Filter objFilter);


        /// <summary>
        /// Conversion de objetos y mapeo de parametros
        /// </summary>
        /// <typeparam name="A">TSource conversion</typeparam>
        /// <typeparam name="T">TDestination conversion</typeparam>
        /// <param name="objActual"></param>
        /// <returns></returns>
        T MapObj<A, T>(A objActual);


        /// <summary>
        /// Conversion de objetos y mapeo de parametros
        /// </summary>
        /// <typeparam name="A">TSource conversion</typeparam>
        /// <typeparam name="T">TDestination conversion</typeparam>
        /// <param name="objActual"></param>
        /// <returns></returns>
        Task<T> MapObjAsyn<A, T>(A objActual);


        /// <summary>
        ///  Configure custom Expresion for mapper
        /// </summary>
        /// <param name="configure">Object mappers</param>
        void CreateMapperExpresion(Action<IMapperConfigurationExpression> configure);


        /// <summary>
        /// Configure custom Expresion for mapper DateTimeOffset
        /// </summary>
        /// <param name="timeZone">time in minutes of zone</param>
        void CreateDateTimeOffsetMapperExpresion(int timeZone);



        /// <summary>
        /// Se eliminar la lista de Entidades de acuerdo a las reglas de negocio y a las dependencias
        /// </summary>
        /// <param name="ListDtos">Listado de Entidades que se eliminar fisicamente</param>
        /// <param name="propertyName">Nombre de la propiedad que obtinene el valor para identificarla</param>
        /// <returns></returns>
        public Task<List<ResponseApi>> DeleteListEntities(IList<ENT> listDtos, string propertyName);
    }
}
