using Microsoft.Azure.Cosmos;
using Microsoft.Extensions.Configuration;
using JOX.Assistant.Models;

namespace JOX.Assistant.Storage
{
    public class CosmosDbService : ICosmosDbService
    {
        private Microsoft.Azure.Cosmos.Container container;
        private readonly IConfiguration _configuration;

        public CosmosDbService(IConfiguration configuration)
        {
            this._configuration = configuration;
            SetConfiguration(this._configuration["CosmosDb:EndPoint"], this._configuration["CosmosDb:Secret"], this._configuration["CosmosDb:DatabaseName"], this._configuration["CosmosDb:ContainerName"]);
        }

        public void SetConfiguration(string accountEndpoint, string accountKey, string databaseName, string containerName)
        {
            var cosmosClient = new CosmosClient(accountEndpoint, accountKey);
            container = cosmosClient.GetContainer(databaseName, containerName);            
        }

        public async Task CreateConversationAsync(Conversation conversation)
        {
            await container.CreateItemAsync(conversation, new PartitionKey(conversation.SessionId));
        }

        public async Task UpdateChatsAsync(Conversation conversation)
        {          

            await container.UpsertItemAsync(conversation, new PartitionKey(conversation.SessionId));
        }

        public async Task<Conversation> GetConversationAsync(string sessionId, string personId)
        {
            try
            {
                var query = new QueryDefinition("SELECT * FROM c WHERE c.SessionId = @sessionId AND c.PersonId = @personId")
                    .WithParameter("@sessionId", sessionId)
                    .WithParameter("@personId", personId);

                var iterator = container.GetItemQueryIterator<Conversation>(query);
                var conversations = new List<Conversation>();

                while (iterator.HasMoreResults)
                {
                    try
                    {
                        var response = await iterator.ReadNextAsync();
                        conversations.AddRange(response);
                    }
                    catch (CosmosException ex) when (ex.StatusCode == System.Net.HttpStatusCode.NotFound)
                    {
                        // Log the exception or handle gracefully
                        Console.WriteLine("No items found in the container.");
                        return null; // Or return a default Conversation object if needed
                    }
                }
                var conversation = conversations.FirstOrDefault();
                if (conversation != null && conversation.Chats.Count > 20)
                {
                    conversation.Chats = conversation.Chats
                        .OrderByDescending(c => c.Date)
                        .Take(20)
                        .ToList();
                }
                return conversation; 
            }
            catch (CosmosException ex)
            {
                // Handle Cosmos DB exceptions
                Console.WriteLine($"Cosmos DB error: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Handle other exceptions
                Console.WriteLine($"Error: {ex.Message}");
                throw;
            }
        }

        public async Task SaveConvesation(Conversation conversation, bool isNew = false)
        {
            conversation.DateModify = DateTime.UtcNow;
            if (string.IsNullOrEmpty(conversation.SessionId))
                conversation.SessionId = conversation.id;
            if (isNew)
            {
                conversation.DateCreate = DateTime.UtcNow;
                await CreateConversationAsync(conversation);
            }
            else
            {
                await UpdateChatsAsync(conversation);
            }
        }
    }
}
