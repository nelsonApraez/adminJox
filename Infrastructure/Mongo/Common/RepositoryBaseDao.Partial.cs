namespace Infrastructure.Mongo.Common
{
    using Domain.Common;
    using Infrastructure.Common;
    using MongoDB.Bson;
    using MongoDB.Driver;
    using Package.Utilities.Net;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    
    public abstract partial class RepositoryBaseDao<T> : BaseRepositoryDao<T>, IRepositoryBase<T>
        where T : class, Domain.Common.Interfaces.IEntities, new()
    {

        /// <summary>
        /// Implementación Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <returns>Retorna el Listado segun los filtros de la entidad <typeparamref name="T"/></returns>
        public ICollection<T> ToList()
        {
            return ToList(null);
        }

        /// <summary>
        /// Listado de Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="parameterOfList">Objeto para parametrizar el listado</param>
        /// <returns>Retorna Listado segun los filtros de la entidad y paginado definido<typeparamref name="T"/></returns>
        public ICollection<T> ToList(ParameterOfList<T> parameterOfList)
        {
            return ConfigureParameterOfList(this._dbCollection.AsQueryable(), parameterOfList).ToList();
        }

        /// <summary>
        /// Objeto de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public T Search(Expression<Func<T, bool>> expression)
        {
            T obj = null;
            if (expression.IsNotNull())
            {
                obj = this._dbCollection.Find(expression).FirstOrDefault();
            }

            return obj;
        }

        /// <summary>
        /// Implementación Objeto de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <param name="includes">Incluciones de relaciones sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna el objeto segun el filtro de la entidad <typeparamref name="T"/></returns>
        public T Search(Expression<Func<T, bool>> expression, params Expression<Func<T, object>>[] includes)
        {
            return Search(expression);
        }

        /// <summary>
        /// Creación del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        public int? Create(T objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {
                //objCreate.Id = ObjectId.GenerateNewId();
                this._dbCollection.InsertOne(objCreate);
                returnCreate = objCreate.Id.ToString().Length;
            }

            return returnCreate;
        }

        /// <summary>
        /// Creación del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objCreate">Lista de Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si Id Insertado o en su defecto numero de registros alterados</returns>
        public int? Create(IEnumerable<T> objCreate)
        {
            int? returnCreate = null;
            if (objCreate.IsNotNull())
            {
                this._dbCollection.InsertMany(objCreate);
                returnCreate = objCreate.ToList().Count;
            }

            return returnCreate;
        }

        /// <summary>
        /// Modificacion del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public bool? Edit(T objEdit)
        {
            bool? returnEdit = null;
            if (objEdit.IsNotNull())
            {
                ObjectId objectId = new ObjectId(objEdit.Id.ToString());
                returnEdit = _dbCollection.ReplaceOne(Builders<T>.Filter.Eq("_id", objectId), objEdit).ModifiedCount > 0;
            }

            return returnEdit;
        }

        /// <summary>
        /// Modificacion del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objEdit">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public bool? Edit(IEnumerable<T> objEdit)
        {
            bool? returnEdit = null;
            if (objEdit.IsNotNull())
            {
                int resultado = 0;
                foreach (var item in objEdit)
                {
                    Edit(item);
                    resultado++;
                }

                returnEdit = resultado != 0;
            }

            return returnEdit;
        }

        /// <summary>
        /// Eliminación del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="objDelete">Entidad especificada <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public bool? Delete(T objDelete)
        {
            bool? returnDelete = null;
            if (objDelete.IsNotNull())
            {
                ObjectId objectId = new ObjectId(objDelete.Id.ToString());
                returnDelete = this._dbCollection.DeleteOne(Builders<T>.Filter.Eq("_id", objectId)).IsAcknowledged;
            }

            return returnDelete;
        }

        /// <summary>
        /// Eliminación del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el registro)</returns>
        public bool? Delete(Expression<Func<T, bool>> expression)
        {
            bool? returnDelete = null;
            if (expression.IsNotNull())
            {
                var objDelete = this._dbCollection.Find(expression).FirstOrDefault();
                if (objDelete.IsNotNull())
                {
                    returnDelete = Delete(objDelete);
                }
                else
                {
                    returnDelete = false;
                }
            }

            return returnDelete;
        }

        /// <summary>
        /// Eliminación maxiva del Objetos de Negocio de la Entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="expression">Filtros sobre la entidad <typeparamref name="T"/></param>
        /// <returns>Retorna si la operacion fue exitosa (altero el o los registros)</returns>
        public bool? DeleteRange(Expression<Func<T, bool>> expression)
        {
            bool? returnDelete = null;
            if (expression.IsNotNull())
            {
                var objDelete = this._dbCollection.Find(expression).ToList();
                if (objDelete.IsNotNull())
                {
                    returnDelete = this._dbCollection.DeleteMany(expression).DeletedCount > 0;
                }
                else
                {
                    returnDelete = false;
                }
            }

            return returnDelete;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="objDelete"></param>
        /// <returns></returns>
        public bool? DeleteRange(IEnumerable<T> objDelete)
        {
            bool? returnDelete = false;

            if (objDelete.IsNotNull())
            {
                foreach (var item in objDelete)
                {
                    returnDelete = Delete(item);
                }
            }

            return returnDelete;
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
                FilterDefinition<T> filter = Builders<T>.Filter.And(expression);
                returnCount = this._dbCollection.Find(filter).CountDocuments();
            }

            return returnCount;
        }
    }
}
