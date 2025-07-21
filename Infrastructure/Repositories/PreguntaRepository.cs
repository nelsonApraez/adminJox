namespace Infrastructure.Repositories
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Pregunta)
    /// </summary>
    [Package.Utilities.Net.BusinessDaoAttribute]
    public partial class PreguntaRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.Pregunta>,
        Domain.Repositories.Interfaces.IPreguntaRepository
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Pregunta)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public PreguntaRepository(Domain.Common.IMainContext contexto) : base(contexto) { } 
    }
}
