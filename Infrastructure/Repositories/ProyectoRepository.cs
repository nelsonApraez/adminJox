using Domain.Common;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Proyecto)
    /// </summary>
    [Package.Utilities.Net.BusinessDaoAttribute]
    public partial class ProyectoRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.Proyecto>,
        Domain.Repositories.Interfaces.IProyectoRepository
    {
        private readonly IMainContext _context;

        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Proyecto)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public ProyectoRepository(IMainContext contexto) : base(contexto)
        {
        }


    }
}
