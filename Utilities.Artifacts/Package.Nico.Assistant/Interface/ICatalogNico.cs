using Nico.Assistant.Models;

namespace Nico.Assistant.Interface
{
    public interface ICatalogNico
    {
        public Task<List<ActionAgentModel>> GetCatalog();
        public Task<List<ActionAgentModel>> GetActionsCatalog();
    }
}
