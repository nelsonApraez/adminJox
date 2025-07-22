using Microsoft.Extensions.Configuration;
using System.Net.Http.Headers;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using JOX.Assistant.Interface;
using JOX.Assistant.Common;
using JOX.Assistant.Models;
using Newtonsoft.Json;

namespace JOX.Assistant.Services
{
    public class IARagService : IIARagService, IDisposable
    {
        private IConfiguration _configuration;
        protected HttpClient httpClient = new();
        private bool isDisposed;
        private IntPtr nativeResource = Marshal.AllocHGlobal(100);
        private string accept;
        private string acceptLanguage;
        private string connection;
        private string contentType;
        private string cookie;
        private string origin;
        private string referer;
        private string secFetchDest;
        private string SecFetchMode;
        private string SecFetchSite;
        private string userAgent;
        private string secChUa;
        private string secChUaMobile;
        private string secChUaPlatform;
        private string url;
        private string folder;

        public IARagService(IConfiguration configuration)
        {
            _configuration = configuration;
            accept = _configuration["ChatIA:Accept"];
            acceptLanguage = _configuration["ChatIA:acceptLanguage"];
            connection = _configuration["ChatIA:connection"];
            contentType = _configuration["ChatIA:content-Type"];
            cookie = _configuration["ChatIA:cookie"];
            origin = _configuration["ChatIA:origin"];
            referer = _configuration["ChatIA:referer"];
            secFetchDest = _configuration["ChatIA:secFetchDest"];
            SecFetchMode = _configuration["ChatIA:SecFetchMode"];
            SecFetchSite = _configuration["ChatIA:SecFetchSite"];
            userAgent = _configuration["ChatIA:userAgent"];
            secChUa = _configuration["ChatIA:secChUa"];
            secChUaMobile = _configuration["ChatIA:secChUaMobile"];
            secChUaPlatform = _configuration["ChatIA:secChUaPlatform"];
            url = _configuration["ChatIA:chatIAEndpoint"];
            folder = _configuration["ChatIA:folder"];

            httpClient.DefaultRequestHeaders.Clear();
            httpClient.DefaultRequestHeaders.Add("Accept", accept);
            httpClient.DefaultRequestHeaders.Add("Accept-Language", acceptLanguage);
            httpClient.DefaultRequestHeaders.Add("Connection", connection);
            httpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(contentType));
            httpClient.DefaultRequestHeaders.Add("Cookie", cookie);
            httpClient.DefaultRequestHeaders.Add("Origin", origin);
            httpClient.DefaultRequestHeaders.Add("Referer", referer);
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", secFetchDest);
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", SecFetchMode);
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", SecFetchSite);
            httpClient.DefaultRequestHeaders.Add("User-Agent", userAgent);
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua", secChUa);
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", secChUaMobile);
            httpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", SecFetchMode);
            httpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", secChUaPlatform);
            httpClient.Timeout = TimeSpan.FromSeconds(60);

        }

        public async Task<string> ConsultarBotIADosConsultas(List<Chat> history, bool WithPromp)
        {
            try
            {
                //esta es una guia de los promp que se deben configurar en el servicio de IA
                /* system_message_chat_conversation = """ 
                {systemPersona} = systemPersona
                { userPersona} = userPersona
                { query_term_language} = parametro que se carga en el servicio
                { follow_up_questions_prompt} = follow_up_questions_prompt_content
                { injected_prompt} = prompt_template
                
                */

                string param = @"
                {
                    ""history"":                             
                           {0}
                        ,
                        ""approach"": ""rrr"",
                        ""overrides"": {
                                    ""semantic_ranker"": true,
                                    ""semantic_captions"": false,
                                    ""top"": 5,
                                    ""suggest_followup_questions"": false,
                                    ""user_persona"": ""{1}"",
                                    ""system_persona"": ""{2}"",
                                    ""ai_persona"": """",
                                    ""response_length"": 3072,
                                    ""response_temp"": 0.6,
                                    ""selected_folders"": ""{5}"",
                                    ""follow_up_questions_prompt_content"": ""{3}"",
                                    ""query_prompt_template"": ""{4}"",
                                    ""selected_tags"": """"
                        }
                }";
                var ltHistory = new List<History>();
                foreach (var item in history)
                {
                    ltHistory.Add(new() { bot= item.BotText, user= item.UserText });                    
                }              
                
                param = param.Replace("{0}", JsonConvert.SerializeObject(ltHistory) ).Replace("{1}", PrompsRAG.sPlantillaSystemPersona)
                             .Replace("{2}", PrompsRAG.sPlantillaUserPersona).Replace("{3}", PrompsRAG.sPlantillaQuestions_prompt_content)
                             .Replace("{4}", PrompsRAG.sPlantillaPrompt_template)
                             .Replace("{5}", folder);

                var data = new StringContent(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<IAModel>(param)), Encoding.UTF8, "application/json");
                using (var httpResponse = await httpClient.PostAsync(url, data))
                {
                    var apiResponseString = await httpResponse.Content.ReadAsStringAsync();

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {

                        return apiResponseString.ToString();
                        //return JsonConvert.DeserializeObject<IAResponse>(apiResponseString.ToString());

                    }
                    return null;
                }
            }
            catch (Exception)
            {

                return null;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (isDisposed)
            {
                return;
            }

            if (disposing)
            {
                // free managed resources
                httpClient.Dispose();
            }

            // free native resources if there are any.
            if (nativeResource != IntPtr.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = IntPtr.Zero;
            }

            isDisposed = true;
        }
        public async Task<string> GetKnowloge(List<Chat> history, string msgdefault)        
        {
            if (!string.IsNullOrEmpty(msgdefault))
                return msgdefault;

            var rag = await ConsultarBotIADosConsultas(history, string.IsNullOrEmpty(msgdefault));
            
            if (!string.IsNullOrEmpty(rag))
            {
                return rag;
            }
            return msgdefault;
        }

        ~IARagService()
        {
            Dispose(false);
        }
    }

}
