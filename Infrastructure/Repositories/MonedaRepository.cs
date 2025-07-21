using Package.Utilities.Net;
using Domain.Common;
using System.Linq;
using Domain.Common.Enums;
using Domain.AggregateModels.Moneda;
using Domain.AggregateModels.Moneda.Specs;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Moneda)
    /// </summary>
    [BusinessDaoAttribute]
    public partial class MonedaRepository :
        Infrastructure.Common.RepositoryBaseDao<Moneda>,
        Domain.Repositories.Interfaces.IMonedaRepository
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Moneda)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public MonedaRepository(IMainContext contexto) : base(contexto) { }

        /// <summary>
        /// Configuarar Objeto, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta
        /// </summary>
        /// <param name="IQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <returns>Querys Dinamicos <paramref name="IQuery"/> para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/></returns>
        protected override IQueryable<Moneda> ConfigureParameterOfList(IQueryable<Moneda> IQuery, ParameterOfList<Moneda> parameterOfList)
        {
            //se identifica si en el filtro esta el campo estado
            return base.ConfigureEstateFilterParameterOfList(IQuery, parameterOfList, MonedaSpecification.ObtenerMonedaActiva, MonedaSpecification.ObtenerMonedaInActiva, nameof(Moneda.Estado));            
        }
    }
}
