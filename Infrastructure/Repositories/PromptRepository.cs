namespace Infrastructure.Repositories
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Prompt)
    /// </summary>
    [Package.Utilities.Net.BusinessDaoAttribute]
    public partial class PromptRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.Prompt>,
        Domain.Repositories.Interfaces.IPromptRepository
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Prompt)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public PromptRepository(Domain.Common.IMainContext contexto) : base(contexto) { } 
    }
}
