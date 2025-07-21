namespace Application.BaseApplicationHelper
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Application.Common;
    using Domain.Common;
    using Domain.Common.Enums;
    using MediatR;
    using Microsoft.AspNetCore.Http;
    using Package.Utilities.Net;

    /// <summary>
    /// Clase base de negocio para todas las entidades dao 
    /// </summary>
    public class BaseApplicationHelper<ENT> : BaseServiceApplication, IBaseApplicationHelper<ENT> where ENT : class, new()
    {
        /// <summary>
        /// Columna de la entidad para ordenar por defecto
        /// </summary>
        protected string PrimaryKeyName;

        /// <summary>
        /// Columna de la entidad para ordenar por defecto
        /// </summary>
        protected string OrderDefaultEntity;


        /// <summary>
        /// Dirección del ordenamiento para OrderDefaultEntity
        /// </summary>
        protected string DirectionDefault;


        /// <summary>
        /// Contiene el contexto de Dao de la Entidad <typeparamref name="ENT"/>
        /// </summary>
        public readonly IRepositoryBase<ENT> Repository;

        IRepositoryBase<ENT> IBaseApplicationHelper<ENT>.Repository => this.Repository;

        public BaseApplicationHelper(IRepositoryBase<ENT> repository, IMediator mediator) : base(typeof(ENT).Name ?? string.Empty, mediator)
        {
            Repository = repository;
            PrimaryKeyName = ObjectBaseExtensions.GetPrimaryKey<ENT>();
            DirectionDefault = EnumerationApplication.Orden.Asc.ToString();
            OrderDefaultEntity = (from asm in typeof(ENT).GetProperties()
                                  where asm.MemberType == System.Reflection.MemberTypes.Property
                                  && asm.Name.ToLower() != "id"
                                  select asm.Name).FirstOrDefault();
        }

        /// <summary>
        /// Creación Async del Objetos de Negocio de la Entidad <typeparamref name="ENT"/>
        /// </summary>
        /// <param name="objCreate">Entidad especificada <typeparamref name="ENT"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        public virtual async Task<int?> CreateAsync(ENT objCreate)
        {
            return await Repository.CreateAsync(objCreate).ConfigureAwait(false);
        }

        /// <summary>
        /// Modificacion Async del Objetos de Negocio de la Entidad <typeparamref name="ENT"/> 
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="ENT"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public virtual async Task<bool?> EditAsync(ENT objEdit)
        {
            var previousObject = await GetPreviousObject(objEdit);
            if (previousObject == null)
                return false;
            var objReturn = await Repository.EditAsync(objEdit).ConfigureAwait(false);
            await CreateAudit(objEdit, previousObject, EnumerationApplication.Operations.Update);
            return objReturn;

        }

        /// <summary>
        /// Eliminación Async del Objetos de Negocio de la Entidad <typeparamref name="ENT"/> 
        /// </summary>
        /// <param name="objDelete">Entidad especificada <typeparamref name="ENT"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public virtual async Task<bool?> DeleteAsync(ENT objDelete)
        {
            if (await GetPreviousObject(objDelete) == null)
                return false;
            var objReturn = await Repository.DeleteAsync(objDelete).ConfigureAwait(false);
            await CreateAudit(objDelete, null, EnumerationApplication.Operations.Remove);
            return objReturn;
        }

        /// <summary>
        /// Obtener Objeto anterior para comparar y generar la descripcion de cambios.
        /// </summary>
        /// <param name="oEntity">Referencia del objeto</param>
        protected async Task<ENT> GetPreviousObject(ENT oEntity)
        {
            var queryFilter = ExpressionHelper.GetCriteriaWhere<ENT>(PrimaryKeyName,
                                                                    EnumerationApplication.OperationExpression.Equals,
                                                                    ObjectBaseExtensions.GetStringKeyValue<ENT>(oEntity));
            return await Repository.SearchAsync(queryFilter).ConfigureAwait(true);
        }

        /// <summary>
        /// Retorna el objeto por el id de la entidad
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual async Task<ENT> GetByIdAsync(string id)
        {
            var queryFilterId = ExpressionHelper.GetCriteriaWhere<ENT>(PrimaryKeyName,
                                                                     EnumerationApplication.OperationExpression.Equals,
                                                                     ObjectBaseExtensions.GetPrimaryKeyValue<ENT>(id));
            return await Repository.SearchAsync(queryFilterId);
        }


        /// <summary>
        /// Cambia el estado de activacion de la entidad de negocio
        /// </summary>
        /// <param name="objEntity">Objeto que se va a cambiar de estado</param>
        /// <param name="propertyState">Nombre del campo estado que va a comparar del objeto</param>
        /// <returns></returns>
        public virtual async Task<List<ResponseApi>> ChangeStateEntity(ENT objEntity, string propertyState)
        {

            //valida que la entidad exista
            var objEntityLocal = objEntity;
            var objEntityBase = await GetPreviousObject(objEntity);
            if (objEntityBase == null)
            {
                AddValidation(StatusCodes.Status400BadRequest, $"{EnumerationMessage.Message.ErrNoEncontrado}", null, objEntity);
            }
            else
            {
                //valida si el estado es el mismo no actualiza nada
                string newState = objEntityLocal.GetType().GetProperty(propertyState).GetValue(objEntityLocal, null).ToString();
                string oldState = objEntityBase.GetType().GetProperty(propertyState).GetValue(objEntityBase, null).ToString();
                if (oldState == newState)
                {
                    AddValidation(StatusCodes.Status400BadRequest, $"{MessageEnums.StateEqual}.{newState}", null, objEntity);
                }
                else
                {
                    //se actualiza correctamente la entidad
                    await EditAsync(objEntity);
                    AddValidation(StatusCodes.Status200OK, $"{MessageEnums.EntityActivate}.{newState}", null, objEntity);
                }
            }
            return ValidationsApi;
        }

        /// <summary>
        /// Validacion de invarianza
        /// </summary>
        /// <param name="objCurrent"></param>
        /// <param name="level"></param>
        /// <returns></returns>
        public virtual async Task<List<ResponseApi>> ValidateRules(ENT objCurrent, EnumerationApplication.Operations level)
        {
            return await Task.FromResult(ValidationsApi);
        }

        public async Task<List<ENT>> ToListAsync()
        {
            return await Repository.ToListAsync();
        }

        public virtual async Task<CustomList<ENT>> ToListPaged(ParameterGetList parameterGetList, Filter objFilter)
        {
            var orderBy = parameterGetList.OrderBy.IsValid() ? parameterGetList.OrderBy : OrderDefaultEntity;
            var direcOrder = parameterGetList.DirecOrder.IsValid() ? parameterGetList.DirecOrder : DirectionDefault;
            return ((objFilter.Filters.IsNotNull() ||
                     (objFilter.Sorts.IsNotNull() && objFilter.Sorts.Count > 1))
                ?
                 await Repository.ToListPaged(new ParameterOfList<ENT>(parameterGetList.Page, parameterGetList.NumberRecords, orderBy, direcOrder, objFilter))
                :
                 await Repository.ToListPaged(new ParameterOfList<ENT>(parameterGetList.Page, parameterGetList.NumberRecords, orderBy, direcOrder)));
        }

        public async Task<List<ENT>> GetDataAsync(Filter objFilter)
        {
            return (objFilter.Sorts.IsNotNull() || objFilter.Filters.IsNotNull()) ?
                     await Repository.ToListAsync(new ParameterOfList<ENT>(objFilter)) :
                     await ToListAsync();
        }



        /// <summary>
        /// Se eliminar la lista de Entidades de acuerdo a las reglas de negocio y a las dependencias
        /// </summary>
        /// <param name="ListDtos">Listado de Entidades que se eliminar fisicamente</param>
        /// <param name="propertyName">Nombre de la propiedad que obtinene el valor para identificarla</param>
        /// <returns></returns>
        public virtual async Task<List<ResponseApi>> DeleteListEntities(IList<ENT> listDtos, string propertyName)
        {
            //definen variables para almacenar los datos de entidades y sus novedades
            List<string> entityDependents = new();
            List<string> entityDeletes = new();
            List<string> entityNoExist = new();
            foreach (var item in listDtos)
            {
                //verifica que no tenga dependencias en otros modelos
                var dependency = ObjectBaseExtensions.GetPropertyValue(item, propertyName);
                //elimina si la entidad existe previamente
                if (await GetPreviousObject(item) != null)
                {
                    try
                    {
                        await DeleteAsync(item);
                        entityDeletes.Add(dependency);
                    }
                    catch (Exception)
                    {
                        //se valida que tiene dependencias de otras entidades                            
                        entityDependents.Add(dependency);
                    }
                }
                else
                {
                    entityNoExist.Add(dependency);
                }
            }
            ResponseForDeleteEntity(entityDependents, entityNoExist, entityDeletes);
            return ValidationsApi;
        }


    }
}
