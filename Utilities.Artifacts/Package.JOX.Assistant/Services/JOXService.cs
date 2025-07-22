using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using JOX.Assistant.Interface;
using JOX.Assistant.Models;
using OpenAI.Chat;

namespace JOX.Assistant.Services
{
    public class JOXService : IJOXService
    {
        private IRulesEngineServices _rulesService;
        private IIARagService _iaService;
        private IJOXStorage _JOXStorage;
        private IAzureAIService _azureIAService;
        private IOpenIAServices _openIaService;
        private IProcessOperation _processOperation;
        private ICatalogJOX _CatalogJOX;
        private List<ActionAgentModel> catalogServices = new();
        private List<ActionAgentModel> actionsServices = new();
        private string promptSystem;
        private string promptAssistant;
        private string promptUser;
        private string questionUser;
        private Conversation currentConversation;

        public IRulesEngineServices RulesService { get => _rulesService; set => _rulesService = value; }
        public IIARagService IaService { get => _iaService; set => _iaService = value; }
        public IJOXStorage JOXStorage { get => _JOXStorage; set => _JOXStorage = value; }
        public IAzureAIService AzureIAService { get => _azureIAService; set => _azureIAService = value; }
        public IOpenIAServices OpenIaService { get => _openIaService; set => _openIaService = value; }
        public IProcessOperation ProcessOperation { get => _processOperation; set => _processOperation = value; }
        public ICatalogJOX CatalogJOX { get => _CatalogJOX; set => _CatalogJOX = value; }
        public List<ActionAgentModel> CatalogServices { get => catalogServices; set => catalogServices = value; }
        public List<ActionAgentModel> ActionsServices { get => actionsServices; set => actionsServices = value; }
        public string PromptSystem { get => promptSystem; set => promptSystem = value; }
        public string PromptAssistantMain;
        public string PromptAssistantTool;
        public string PromptUser { get => promptUser; set => promptUser = value; }
        public string QuestionUser { get => questionUser; set => questionUser = value; }
        public Conversation CurrentConversation { get => currentConversation; set => currentConversation = value; }

        public JOXService(IRulesEngineServices rulesEngineServices, IIARagService iAService, IJOXStorage joxStorage,
            IAzureAIService azureAIService, IOpenIAServices openIAServices, IConfiguration configuration, IProcessOperation processOperation,
            ICatalogJOX catalogJOX)
        {
            RulesService = rulesEngineServices;
            IaService = iAService;
            _JOXStorage = joxStorage;
            AzureIAService = azureAIService;
            OpenIaService = openIAServices;
            ProcessOperation = processOperation;
            _CatalogJOX = catalogJOX;
            CatalogServices = catalogJOX.GetCatalog().GetAwaiter().GetResult();
            ActionsServices = catalogJOX.GetActionsCatalog().GetAwaiter().GetResult();
            PromptSystem = configuration["JOX:PromptSystem"];
            PromptAssistantMain = configuration["JOX:PromptAssistantMaster"];
            PromptAssistantTool = configuration["JOX:PromptAssistant"];
            PromptUser = configuration["JOX:PromptUser"];
        }

        public async Task ProcessMessageStart(Func<string, Task> fncallback, string text = "")
        {
            await fncallback("Hola soy Agente Inteligente JOX");
        }

        public async Task<ActionAgentModel> DetectIntentionService(string promAss, string prompUser)
        {
            var messages = new List<ChatMessage>()
            {
                new SystemChatMessage(PromptSystem),
                new AssistantChatMessage(string.IsNullOrEmpty(promAss) ? GetprompConversationBase(PromptAssistantMain) : GetprompConversationBase(promAss)),
                new UserChatMessage(string.IsNullOrEmpty(prompUser) ? GetprompConversationBase(PromptUser) : GetprompConversationBase(prompUser))
            };
            var scoreModelIA = new ActionAgentModel();
            try
            {
                var completion = await OpenIaService.CreateChatCompletionAsync(messages);
                scoreModelIA = JsonConvert.DeserializeObject<ActionAgentModel>(completion);
                CurrentConversation.Tags = scoreModelIA.parameters;
            }
            catch (Exception)
            {
                scoreModelIA.message = "None";
            }
            return scoreModelIA;
        }

        public async Task ProcessAndValidateConvesation(Conversation convesationBase, Func<ResponseJOX, Task> fncallback)
        {
            CurrentConversation = convesationBase;
            await fncallback(new ResponseJOX("JOX Service Working", ""));
        }

        private string GetprompConversationBase(string prompt)
        {
            return prompt ?? "";
        }
    }
}
