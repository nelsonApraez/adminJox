using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Nico.Assistant.Interface;
using Nico.Assistant.Models;
using OpenAI.Chat;


namespace Nico.Assistant.Services
{
    public class NicoService : INicoService
    {
        private IRulesEngineServices _rulesService;
        private IIARagService _iaService;
        private INicoStorage _nicoStorage;
        private IAzureAIService _azureIAService;
        private IOpenIAServices _openIaService;
        private IProcessOperation _processOperation;
        private ICatalogNico _catalogNico;
        private List<ActionAgentModel> catalogServices =new();
        private List<ActionAgentModel> actionsServices = new();
        private string promptSystem;
        private string promptAssistant;
        private string promptUser;
        private string questionUser;
        private Conversation currentConversation;

        public IRulesEngineServices RulesService { get => _rulesService; set => _rulesService = value; }
        public IIARagService IaService { get => _iaService; set => _iaService = value; }
        public INicoStorage NicoStorage { get => _nicoStorage; set => _nicoStorage = value; }
        public IAzureAIService AzureIAService { get => _azureIAService; set => _azureIAService = value; }
        public IOpenIAServices OpenIaService { get => _openIaService; set => _openIaService = value; }
        public IProcessOperation ProcessOperation { get => _processOperation; set => _processOperation = value; }
        public ICatalogNico CatalogNico { get => _catalogNico; set => _catalogNico = value; }
        public List<ActionAgentModel> CatalogServices { get => catalogServices; set => catalogServices = value; }
        public List<ActionAgentModel> ActionsServices { get => actionsServices; set => actionsServices = value; }
        public string PromptSystem { get => promptSystem; set => promptSystem = value; }
        public string PromptAssistantMain;
        public string PromptAssistantTool;
        public string PromptUser { get => promptUser; set => promptUser = value; }
        public string QuestionUser { get => questionUser; set => questionUser = value; }
        public Conversation CurrentConversation { get => currentConversation; set => currentConversation = value; }

        public NicoService(IRulesEngineServices rulesEngineServices, IIARagService iAService, INicoStorage nicoStorage,
            IAzureAIService azureAIService, IOpenIAServices openIAServices, IConfiguration configuration, IProcessOperation processOperation,
            ICatalogNico catalogNico)
        {
            RulesService = rulesEngineServices;
            IaService = iAService;
            NicoStorage = nicoStorage;
            AzureIAService = azureAIService;
            OpenIaService = openIAServices;
            ProcessOperation = processOperation;
            CatalogNico = catalogNico;
            CatalogServices = CatalogNico.GetCatalog().GetAwaiter().GetResult();
            ActionsServices = CatalogNico.GetActionsCatalog().GetAwaiter().GetResult();
            PromptSystem = configuration["Nico:PromptSystem"];
            PromptAssistantMain = configuration["Nico:PromptAssistantMaster"];
            PromptAssistantTool = configuration["Nico:PromptAssistant"];
            PromptUser = configuration["Nico:PromptUser"];           

        }

        public async Task ProcessMessageStart(Func<string, Task> fncallback, string text = "")
        {
            await fncallback("Hola soy Agente Inteligente");
            //await fncallback(text==string.Empty? "para mejorar tu experiencia indícanos tu nombre o usuario de red:" : text);
        }

        public async Task<ActionAgentModel> DetectIntentionService(string promAss,string prompUser)
        {
            // detect intentions by service catalog            
            var messages = new List<ChatMessage>()
            {
                new SystemChatMessage(PromptSystem ),
                new AssistantChatMessage(string.IsNullOrEmpty(promAss)?GetprompConversationBase(PromptAssistantMain) : GetprompConversationBase(promAss) ),
                new UserChatMessage(string.IsNullOrEmpty(prompUser)? GetprompConversationBase(PromptUser):GetprompConversationBase(prompUser) )
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

        


        public async Task ProcessAndValidateConvesation(Conversation convesationBase, Func<ResponseNico, Task> fncallback)
        {
            CurrentConversation = convesationBase;
            //se obtiene el historico de la conversacion
            var convDb = await NicoStorage.GetConvesation(CurrentConversation.id, CurrentConversation.PersonId);
            if (convDb != null) {
                CurrentConversation.Chats.Clear();
                CurrentConversation.Chats.AddRange(convDb.Chats);
                CurrentConversation.ScoredIntent = convDb.ScoredIntent;
                CurrentConversation.TypeOperation = convDb.TypeOperation;
                currentConversation.From = convDb.From;
                currentConversation.RagText = convDb.RagText;
            }
            else
            {
                //aca se consulta el catalogo de servicios para mostarlos
                CurrentConversation.From.Who = CurrentConversation.UserText;
                await NicoStorage.SaveConvesation(CurrentConversation, convDb == null);
                var completion = await OpenIaService.CreateChatCompletionAsync(new List<ChatMessage>()
                {
                    new SystemChatMessage("You are an automation system that identifies services the output is only message" ),
                    new AssistantChatMessage("Tu eres un agente de servicios empresariales, genera un mensaje mostrando como se pueden usar los servicios con palabras claves maximo 100 caracteres por descripcion sin nombre sin formato y sin parametros,la salida debe ser un solo parrafo" ),
                    new UserChatMessage(JsonConvert.SerializeObject(this.CatalogServices)  )
                });
                await fncallback(new ResponseNico(completion, CurrentConversation.ScoredIntent) { Mode = ModeNico.Continue });
                return;
            }
            var currentChat = new Chat { UserText = CurrentConversation.UserText, Mode = "Info" };
            CurrentConversation.CurrentText = CurrentConversation.UserText;
            QuestionUser = CurrentConversation.UserText;
            CurrentConversation.Chats.Add(currentChat);
            //obtiene de los ultimos chats del hilo de la conversacion
            QuestionUser = GetCurrentQuestion(convDb, QuestionUser);
            CurrentConversation.UserText= QuestionUser;         
            // detect intentions by service catalog por la pregunta actual           
            var scoreModelIA = await DetectIntentionService("","");
            CurrentConversation.Tags = scoreModelIA.parameters;
            CurrentConversation.ScoredIntent = scoreModelIA.name;
            CurrentConversation.TypeOperation = scoreModelIA.mode;
            //si se detecta una recomendacion de servicio
            if (!string.IsNullOrEmpty(scoreModelIA.message) && scoreModelIA.message!="None" && CurrentConversation.TypeOperation == "continue")
            {
                await fncallback(new ResponseNico(scoreModelIA.message, CurrentConversation.ScoredIntent) { Mode = ModeNico.Continue });
            }
            else
            {
                //si la convesacion se mantiene en el hilo de la intencion se piden los datos de la conversacion
                if (scoreModelIA.message != "None")
                {
                    currentChat.BotText = scoreModelIA.message;
                    await fncallback(new ResponseNico(scoreModelIA.message, CurrentConversation.ScoredIntent) { Mode = ModeNico.Continue });
                    _ = Task.Run(async () =>
                    {
                        await SaveAndCompleteConversation(CurrentConversation, convDb, currentChat);
                    });
                    return;
                }
            }            
            
            //realiza la clasificacion ScoreIdent
            ClasifyIntention();                
            try
            {
                switch (CurrentConversation.TypeOperation)
                {
                    case "RulesEnginer":
                    case "Operation":
                    case "ModelIA":
                        //se envia a procesar el mensaje si no es RAG
                        var actionmodel = GetServiceByName(scoreModelIA.name);
                        await ProcessOperationConversation(actionmodel, fncallback);
                        break;
                    default:
                        //se recibe el mensaje de enviar a RAG                     
                        await fncallback(new ResponseNico(await ProcessRagMessage(), CurrentConversation.ScoredIntent) { Mode = ModeNico.Warning });
                        break;
                }

            }
            catch (Exception)
            {
                await fncallback(new ResponseNico("No se pudo generar la solicitud con su intención, por favor intente más tarde", CurrentConversation.ScoredIntent) { Mode = ModeNico.Error });
            }

            _ = Task.Run(async () =>
            {
                await SaveAndCompleteConversation(CurrentConversation, convDb, currentChat);
            });
        }

        


        private async Task ProcessOperationConversation(ActionAgentModel scoreModelIA, Func<ResponseNico, Task> fncallback)
        {
            //antes de procesar la accion debe validar si tiene alguna tarea por completar y procesarla con el modelo de IA para erriquecer el input
            var respBefore = string.Empty;
            if (!string.IsNullOrEmpty(scoreModelIA.before))
            {
                await fncallback(new ResponseNico("Aguarda tardare un momento", CurrentConversation.ScoredIntent) { Mode = ModeNico.Continue });
                foreach (var item in scoreModelIA.before.Split('|'))
                {
                    if (!item.Contains("{{rag}}") && !item.Contains("{{agentservice}}"))
                        respBefore = await CallModelIAservice(await ProcessIntention(item, fncallback));
                    else
                        respBefore = await ProcessIntention(item, fncallback);
                    respBefore = respBefore.Replace("{{ragresponse}}", currentConversation.RagText );
                    if (!string.IsNullOrEmpty(respBefore))
                        QuestionUser = QuestionUser + ";" + respBefore;
                }                
            }            
            //si tiene acciones antes pero la respuesta fue vacia debe pedir los campos
            if (!string.IsNullOrEmpty(scoreModelIA.before) && string.IsNullOrEmpty(respBefore))
                return;
            //primero debe actualizar los valores de los parametros
            if (CurrentConversation.Tags.Count > 0)
            {
                var modelParamBase = JsonConvert.SerializeObject(GetServiceByName(scoreModelIA.name));
                var modelParam = JsonConvert.DeserializeObject<ActionAgentModel>(modelParamBase);
                //esto se hace para minimizar el prompt de las propiedades que no se reuieren cuando se arman los parametros
                modelParam.before = "";
                modelParam.after = "";
                var promBase = PromptAssistantTool.Replace("{{services}}", JsonConvert.SerializeObject(modelParam));
                promBase = GetprompConversationBase(promBase);
                //se llama al servicio para que detecte todos los parametros
                var scoreModel = await DetectIntentionService(promBase,"");
                //si detenta que falta algun paramtro lo debe mantener en la conversacion para el siguiente paso
                if (scoreModel.message != "None")
                {
                    CurrentConversation.RagText = scoreModel.message;
                    await fncallback(new ResponseNico(scoreModel.message, CurrentConversation.ScoredIntent) { Mode = ModeNico.Continue });
                    return;
                }
            }            

            if (string.IsNullOrEmpty(respBefore))
                respBefore = CurrentConversation.UserText;
            var responsetool = string.Empty;
            switch (scoreModelIA.mode)
            {
                case "RulesEnginer":
                case "Operation":
                case "ModelIA":
                    responsetool = await ProcessRulesMessage(respBefore, scoreModelIA,CurrentConversation.Tags);
                    CurrentConversation.TypeOperation = CurrentConversation.TypeOperation + ",end";
                    CurrentConversation.ScoredIntent = CurrentConversation.ScoredIntent + ",end";
                    CurrentConversation.RagText = responsetool;
                    break;
                default:
                    await fncallback(new ResponseNico(await ProcessRagMessage(), CurrentConversation.ScoredIntent) { Mode = ModeNico.Warning });
                    return;
            }
            //valida si despues debe hacer alguna tarea para enrriquecer la respuesta
            if (!string.IsNullOrEmpty(scoreModelIA.after))
            {
                if (!scoreModelIA.after.Contains("{{rag}}") && !scoreModelIA.after.Contains("{{agentservice}}"))
                    responsetool = await CallModelIAservice(await ProcessIntention(scoreModelIA.after.Replace("{{responsetool}}", responsetool), fncallback));                
                else
                    responsetool = await ProcessIntention(scoreModelIA.after.Replace("{{responsetool}}", responsetool), fncallback);
            }
            if (!string.IsNullOrEmpty(responsetool))
            {
                currentConversation.RagText = responsetool;
                await fncallback(new ResponseNico(responsetool, CurrentConversation.ScoredIntent) { Mode = ModeNico.Ok });
            }
        }


        public ActionAgentModel GetServiceByName(string name)
        {
            var actionmodel = new ActionAgentModel();
            if (ActionsServices.Any(a => a.name == name))
            {
                actionmodel = ActionsServices.FirstOrDefault(a => a.name == name);
            }
            return actionmodel;
        }


        public async Task<string> ProcessIntention(string acctionmodel, Func<ResponseNico, Task> fncallback)
        {
            //metodo que procesa la intension del servicio de acurdo a la accion del servicio
            //su proposito es crear los valores para procesar el tool
            /* se usan para crear los promps en los servicios para crear una comunicacion mas asertiva            
            {{rag}} es para solicitar la respuesta de rag
            {{agenttool}} es la respuesta de agent tool es decir la respuesta de integracion externa
            {{responsetool}} es la respuesta del servicio externo de integracion
            {{agentservice}} es la respuesta del servicio identificado que resolvera la necesidad del usuario
            */
            //primero revisa la accion previa
            if (string.IsNullOrEmpty(acctionmodel))
                return string.Empty;
            var actionresp = GetprompConversationBase(acctionmodel);
            //identifica si debe hacer consulta a RAG
            if (actionresp.Contains("{{rag}}"))
            {
                var rag = await IaService.GetKnowloge(new() { new() {BotText="",UserText= actionresp.Replace("{{rag}}", "") } } , "");
                if (!string.IsNullOrEmpty(rag))
                {
                    var rgResp = JsonConvert.DeserializeObject<IAResponse>(rag);
                    CurrentConversation.RagText = rgResp.Answer;
                    actionresp = actionresp.Replace("{{rag}}", "answer rag: "+ rgResp.Answer);
                }
            }
            //identifica identifica el servicio que debe usar con sus parametros
            if (actionresp.Contains("{{agentservice}}"))
            {               
                
                //se llama al servicio para que detecte todos los parametros
                var scoreModel = await DetectIntentionService("", PromptUser.Replace("{{question}}", actionresp.Replace("{{agentservice}}", "")));
                //si detenta un servicio lo debe mantener en la conversacion para el siguiente paso
                if (scoreModel.message != "None")
                {
                    CurrentConversation.RagText = scoreModel.message;
                    await fncallback(new ResponseNico(scoreModel.message, CurrentConversation.ScoredIntent) { Mode = ModeNico.Continue });
                    return string.Empty;
                }
                else
                {
                    //llama al servicio para solicitar la informacion complementaria
                    var resp = await ProcessRulesMessage(actionresp, scoreModel, scoreModel.parameters);
                    actionresp = actionresp.Replace("{{agentservice}}", "answer agentservice: "+resp);
                }
            }
            return actionresp;
        }


        public async Task<string> ProcessRulesMessage(string userText, ActionAgentModel actionModel, Dictionary<string, string> atribb )
        {
            switch (actionModel.mode)
            {
                case "RulesEnginer":
                    RequestRulesEngine request = new()
                    {
                        ConversationId = CurrentConversation.id ,
                        PersonId = CurrentConversation.PersonId ,
                        ResourceName = CurrentConversation.SessionId,
                        Action = actionModel.name,
                        Intention = actionModel.name,
                        Params = new Params
                        {
                            Organization = "Your Company",
                            Value = ""
                        },
                        Content = new Content
                        {
                            Text = CurrentConversation.RagText,
                            Value = userText
                        }
                    };
                    //llama al motor de reglas
                    var devOpsService = RulesService.DispatchWorkFlow(request);
                    if (devOpsService != null)
                    {
                        CurrentConversation.RagText = devOpsService;
                        return devOpsService;
                    }
                    else
                    {
                        return "No se pudo generar la solicitud con su intención, por favor intente más tarde";
                    }
                case "Operation":
                    //cuando son servicios de integracion internos
                    var process = new ActionAgentModel
                    {
                        name = actionModel.name,
                        message = userText,
                        mode = actionModel.mode,
                        parameters = atribb
                    };
                    var processResp = await ProcessOperation.ExecuteOperation(process);
                    return processResp.Message;                    
            }
            return string.Empty;
        }

        public async Task<string> CallModelIAservice(string promp)
        {
            if (string.IsNullOrEmpty(promp))
                return string.Empty;
            var messages = new List<ChatMessage>()
                        {
                            new UserChatMessage( promp )
                        };
            try
            {
                return await OpenIaService.CreateChatCompletionAsync(messages);
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        private async Task<string> ProcessRagMessage()
        {
            //se recibe el mensaje de enviar a RAG
            var rag = await IaService.GetKnowloge(CurrentConversation.Chats, "");
            if (!string.IsNullOrEmpty(rag))
            {
                var rgResp = JsonConvert.DeserializeObject<IAResponse>(rag);
                CurrentConversation.RagText = rgResp.Answer;
                return ((CurrentConversation.TypeChannel == "directline" && CurrentConversation.From.Id.IndexOf("dlchat") < 0) ? rag : rgResp.Answer);
            }
            return "¡Lo lamento! No entendí tu pregunta. Por favor, intente preguntar de una manera diferente.";
        }
        public string GetprompConversationBase(string promp)
        {
            /* se usan para crear los promps en los servicios para crear una comunicacion mas asertiva
            {{question}} es la pregunta actual o contexto de la conversacion
            {{services}} es la lista de servicios o catalogo
            {{historyuser}} es history convesational solo del usuario
            {{historyagent}} es history convesational solo de las repuestas del agente            
            {{history}} es el historico completo conversacional
            {{ragresponse}} rag response es la respuesta de RAG
            {{user}} rag response es la respuesta de RAG
            */
            var promtresult = promp;
            if (string.IsNullOrEmpty(promp))
                return string.Empty;
            if (promtresult.Contains("{{services}}"))
                promtresult = promtresult.Replace("{{services}}", JsonConvert.SerializeObject(CatalogServices));
            if (promtresult.Contains("{{question}}"))
                promtresult = promtresult.Replace("{{question}}", QuestionUser);
            if (promtresult.Contains("{{ragresponse}}"))
                promtresult = promtresult.Replace("{{ragresponse}}", currentConversation.RagText);            
            if (promtresult.Contains("{{historyuser}}"))
                promtresult = promtresult.Replace("{{historyuser}}", string.Join("; ", CurrentConversation.Chats.SelectMany(a => a.UserText)));
            if (promtresult.Contains("{{historyagent}}"))
                promtresult = promtresult.Replace("{{historyagent}}", string.Join("; ", CurrentConversation.Chats.SelectMany(a => a.BotText)));
            if (promtresult.Contains("{{history}}"))
                promtresult = promtresult.Replace("{{history}}", string.Join("; ", CurrentConversation.Chats.SelectMany(a => "question -> " + a.UserText + " : answer -> " + a.BotText)));
            if (promtresult.Contains("{{user}}"))
                promtresult = promtresult.Replace("{{user}}", currentConversation.From.Who);
            return promtresult;
        }

        public async Task SaveAndCompleteConversation(Conversation convesation, Conversation convDb, Chat currentChat)
        {
            //se limpiar el historico y se recarga con la ultima inteccion de RAG
            if (convDb != null)
            {
                convesation.Chats.Clear();
                convesation.Chats.AddRange(convDb.Chats);
                convesation.From = convDb.From;
                convesation.DateCreate = convDb.DateCreate;
            }
            if (!string.IsNullOrEmpty(convesation.RagText))
                currentChat.BotText = convesation.RagText;
            currentChat.Mode = convesation.ScoredIntent;
            currentChat.Type = convesation.TypeOperation;
            currentChat.Tags = convesation.Tags;
            convesation.Chats.Add(currentChat);
            var chatUser = convesation.Chats.Select(a => a.UserText).ToList();
            var calification = await AzureIAService.AnalyzeSentimentAsync(string.Join(" ; ", chatUser));
            convesation.Calification = calification.Item1;
            currentChat.Calification = calification.Item2;
            //se debe guardar la conversacion
            await NicoStorage.SaveConvesation(convesation, convDb == null);
        }


        public string GetCurrentQuestion(Conversation convDb, string question)
        {
            var qReps = question; 
            var lisCha = new List<string>();
            for (int i = CurrentConversation.Chats.Count - 1; i >= 0; i--)
            {
                if (!CurrentConversation.Chats[i].Mode.Contains(",end"))
                {
                    lisCha.Add(CurrentConversation.Chats[i].UserText);
                }
                else
                    break;
            }
            lisCha.Reverse();
            qReps = string.Join(" ; ", lisCha);        
            return qReps;
        }


        public void ClasifyIntention()
        {
            //determina si la intencion debe ser para el agente IA
            if (CurrentConversation.ScoredIntent != "agent" && (CurrentConversation.CurrentText.ToLower().Contains("@nico") || CurrentConversation.CurrentText.ToLower().Contains("@agent")))
            {
                CurrentConversation.ScoredIntent = "agent";
            }
            if (CurrentConversation.ScoredIntent != "None" && CurrentConversation.CurrentText.ToLower().Contains("@rag"))
            {
                CurrentConversation.ScoredIntent = "None";
            }
            if (string.IsNullOrWhiteSpace(CurrentConversation.RagText) && CurrentConversation.ScoredIntent == "agent")
            {
                CurrentConversation.RagText = CurrentConversation.CurrentText;
            }
            //si lo debe enviar al motor de reglas
            if (CurrentConversation.ScoredIntent != "None" && CurrentConversation.CurrentText.ToLower().Contains("@engine"))
            {
                CurrentConversation.RagText = CurrentConversation.CurrentText;
            }
            //si no tiene identificada la intencion lo envia a open IA para extraerla del contexto
            if (CurrentConversation.ScoredIntent == "None")
            {
                CurrentConversation.TypeOperation = "RAG";
            }
            if (CurrentConversation.ScoredIntent == "agent")
            {
                CurrentConversation.TypeOperation = "agent";
            }
        }

    }
}
