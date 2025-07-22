using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using JOX.Assistant.Interface;
using JOX.Assistant.Models;


namespace JOX.Assistant.Services {
    public class CatalogJOXServices : ICatalogJOX
    {
        private IConfiguration _configuration;

        public CatalogJOXServices(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        public async Task<List<ActionAgentModel>> GetCatalog()
        {
            var catalog = JsonConvert.DeserializeObject<List<ActionAgentModel>>(_configuration["JOX:CatalogServices"]);             
            return await Task.FromResult(catalog);
        }

        public async Task<List<ActionAgentModel>> GetActionsCatalog()
        {
            var catalog = JsonConvert.DeserializeObject<List<ActionAgentModel>>(_configuration["JOX:CatalogActions"]);
            return await Task.FromResult(catalog);
        }
    }
}

