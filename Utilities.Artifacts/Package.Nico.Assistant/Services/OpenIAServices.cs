using Azure.AI.OpenAI;
using Microsoft.Extensions.Configuration;
using Nico.Assistant.Interface;
using OpenAI.Chat;
using System;
using System.ClientModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nico.Assistant.Services
{
    public class OpenIAServices: IOpenIAServices
    {
        private readonly int maxTokens = 10024;
        private string model = "gpt-35-turbo-16k";
        private AzureOpenAIClient azureClient;
        private ChatClient chatClient;

        public OpenIAServices(IConfiguration _configuration)
        {

            var endPointUrl = _configuration["OpenIA:OpenIAEndpoint"];
            var endPointKey = _configuration["OpenIA:OpenIAAPIKey"];            
            maxTokens = Convert.ToInt16( _configuration["OpenIA:maxTokens"]);
            model = _configuration["OpenIA:model"];
            SetConfiguration(endPointUrl, endPointKey, model);
        }

        public void SetConfiguration(string urlOpenIa, string apiKey, string modelDep)
        {            
            azureClient = new(
           new Uri(urlOpenIa),
           new ApiKeyCredential(apiKey), new AzureOpenAIClientOptions(AzureOpenAIClientOptions.ServiceVersion.V2025_01_01_Preview  ) {  });
            chatClient = azureClient.GetChatClient(modelDep);
            model = modelDep;
        }


        public async Task<string> CreateChatCompletionAsync(IList<ChatMessage> requests, string modelFormat = "text", float temperature = (float)0.1)
        {

            var chatCompletionsOptions = new ChatCompletionOptions()
            {

                Temperature = temperature,
                MaxOutputTokenCount = maxTokens,
                FrequencyPenalty = 0,
                PresencePenalty = 0,
                ResponseFormat = modelFormat.ToLower() == "json" ? ChatResponseFormat.CreateJsonObjectFormat() : ChatResponseFormat.CreateTextFormat()
            };
            var resr = await chatClient.CompleteChatAsync(requests.ToArray(), chatCompletionsOptions);
            return resr.Value.Content.First().Text.Replace("```json", "").Replace("```", "");
        }
    }
}
