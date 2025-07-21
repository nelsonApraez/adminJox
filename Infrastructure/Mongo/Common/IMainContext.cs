using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;

namespace Infrastructure.Mongo.Common
{
    public partial interface IMainContext : Domain.Common.IMainContext
    {
        /// <summary>
        /// Obtener Intancia de la Collection de la Base de Datos
        /// </summary>
        /// <typeparam name="TEntity">Entidad de Negocio</typeparam>
        /// <returns>Retorna la Intancia de la Collection de la Base de Datos</returns>
        IMongoCollection<TEntity> GetCollection<TEntity>();
    }
}
