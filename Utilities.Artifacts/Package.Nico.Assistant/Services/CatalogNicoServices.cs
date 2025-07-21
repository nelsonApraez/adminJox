using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nico.Assistant.Interface;
using Nico.Assistant.Models;


namespace Nico.Assistant.Services
{
    public class CatalogNicoServices : ICatalogNico
    {
        private IConfiguration _configuration;

        public CatalogNicoServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<ActionAgentModel>> GetCatalog()
        {
            var catalog = JsonConvert.DeserializeObject<List<ActionAgentModel>>(_configuration["Nico:CatalogServices"]);             
            return await Task.FromResult(catalog);
        }

        public async Task<List<ActionAgentModel>> GetActionsCatalog()
        {
            var catalog = JsonConvert.DeserializeObject<List<ActionAgentModel>>(_configuration["Nico:CatalogActions"]);
            return await Task.FromResult(catalog);
        }
    }
}
