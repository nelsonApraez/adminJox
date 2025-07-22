using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Configuration;
using JOX.Assistant.Interface;
using System.Runtime.CompilerServices;


namespace JOX.Assistant.Services
{
    public class AzureAIService: IAzureAIService
    {
        private TextAnalyticsClient _textAnalyticsClient;        

        public AzureAIService(IConfiguration configuration)
        {                
            SetConfiguration(configuration["AzureIA:IAEndpoint"], configuration["AzureIA:IAAPIKey"]);
        }

        public void SetConfiguration(string endpoint, string apiKey)
        {
            try
            {
                var credentials = new AzureKeyCredential(apiKey);
                _textAnalyticsClient = new TextAnalyticsClient(new Uri(endpoint), credentials);                

            }
            catch (Exception ex)
            {
                throw new InvalidOperationException("Failed to initialize AzureAIService.", ex);
            }
        }


        //Analyzing the sentiment of a single document
        public async Task<Tuple<string,string>> AnalyzeSentimentAsync(string content)
        {
            if (string.IsNullOrEmpty(content))
            {
                return  Tuple.Create("neutral", "neutral");                
            }
            try
            {
                DocumentSentiment documentSentiment = await _textAnalyticsClient.AnalyzeSentimentAsync(content);                
                return Tuple.Create(documentSentiment.Sentiment.ToString(), documentSentiment.Sentences.LastOrDefault().Sentiment.ToString());
            }
            catch (Exception ex)
            {
                return Tuple.Create("neutral", "neutral");
            }
        }


        /// <summary>
        /// Extracts key phrases from text content.
        /// </summary>
        /// <param name="content">The text to analyze.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of extracted key phrases.</returns>
        public async Task<List<string>> ExtractKeyPhrasesAsync(string content)
        {
            try
            {
                var response = await _textAnalyticsClient.ExtractKeyPhrasesAsync(content);
                return new List<string>(response.Value);
            }
            catch (RequestFailedException ex)
            {             
                throw new InvalidOperationException("Failed to extract key phrases.", ex);
            }
            catch (Exception ex)
            {             
                throw;
            }
        }

        /// <summary>
        /// Extracts entities from text content.
        /// </summary>
        /// <param name="content">The text to analyze.</param>
        /// <returns>A task representing the asynchronous operation, containing a list of recognized entities.</returns>
        public async Task<List<string>> ExtractEntitiesAsync(string content)
        {
            try
            {
                var response = await _textAnalyticsClient.RecognizeEntitiesAsync(content);
                var entities = new List<string>();
                foreach (var entity in response.Value)
                {
                    entities.Add(entity.Text);
                }
                return entities;
            }
            catch (RequestFailedException ex)
            {                
                throw new InvalidOperationException("Failed to extract entities.", ex);
            }
            catch (Exception ex)
            {             
                throw;
            }
        }
    }
}
