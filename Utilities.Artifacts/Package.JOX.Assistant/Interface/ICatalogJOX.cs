using JOX.Assistant.Models;

namespace JOX.Assistant.Interface
{
    public interface ICatalogJOX
    {
        public Task<List<ActionAgentModel>> GetCatalog();
        public Task<List<ActionAgentModel>> GetActionsCatalog();
    }
}
