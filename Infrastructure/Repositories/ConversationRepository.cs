namespace Infrastructure.Repositories
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Conversation)
    /// </summary>
    [Package.Utilities.Net.BusinessDaoAttribute]
    public partial class ConversationRepository :
        Infrastructure.Cosmos.Common.RepositoryBaseDao<Domain.AggregateModels.Conversation>,
        Domain.Repositories.Interfaces.IConversationRepository
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Conversation)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public ConversationRepository(Infrastructure.Cosmos.Common.IMainContext contexto) : base(contexto) { } 
    }
}
