using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Net;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;
using Domain.Services.Interfaces;
using Domain.Models;
using Domain.AggregateModels;

namespace Infrastructure.Service.OpenIA
{
    public class RagIAService : IDisposable, IragIAServices
    {
        protected HttpClient HttpClient = new();
        private bool isDisposed;
        private nint nativeResource = Marshal.AllocHGlobal(100);
        private readonly string _accept;
        private readonly string _acceptLanguage;
        private readonly string _connection;
        private readonly string _contentType;
        private readonly string _cookie;
        private readonly string _origin;
        private readonly string _referer;
        private readonly string _secFetchDest;
        private readonly string _secFetchMode;
        private readonly string _secFetchSite;
        private readonly string _userAgent;
        private readonly string _secChUa;
        private readonly string _secChUaMobile;
        private readonly string _secChUaPlatform;
        private readonly string _url;

        public RagIAService(IConfiguration configuration)
        {
            _accept = configuration["ChatIA:Accept"];
            _acceptLanguage = configuration["ChatIA:acceptLanguage"];
            _connection = configuration["ChatIA:connection"];
            _contentType = configuration["ChatIA:content-Type"];
            _cookie = configuration["ChatIA:cookie"];
            _origin = configuration["ChatIA:origin"];
            _referer = configuration["ChatIA:referer"];
            _secFetchDest = configuration["ChatIA:secFetchDest"];
            _secFetchMode = configuration["ChatIA:SecFetchMode"];
            _secFetchSite = configuration["ChatIA:SecFetchSite"];
            _userAgent = configuration["ChatIA:userAgent"];
            _secChUa = configuration["ChatIA:secChUa"];
            _secChUaMobile = configuration["ChatIA:secChUaMobile"];
            _secChUaPlatform = configuration["ChatIA:secChUaPlatform"];
            _url = configuration["ChatIA:chatIAEndpoint"];

            HttpClient.DefaultRequestHeaders.Clear();
            HttpClient.DefaultRequestHeaders.Add("Accept", _accept);
            HttpClient.DefaultRequestHeaders.Add("Accept-Language", _acceptLanguage);
            HttpClient.DefaultRequestHeaders.Add("Connection", _connection);
            HttpClient.DefaultRequestHeaders
                .Accept
                .Add(new MediaTypeWithQualityHeaderValue(_contentType));
            HttpClient.DefaultRequestHeaders.Add("Cookie", _cookie);
            HttpClient.DefaultRequestHeaders.Add("Origin", _origin);
            HttpClient.DefaultRequestHeaders.Add("Referer", _referer);
            HttpClient.DefaultRequestHeaders.Add("Sec-Fetch-Dest", _secFetchDest);
            HttpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", _secFetchMode);
            HttpClient.DefaultRequestHeaders.Add("Sec-Fetch-Site", _secFetchSite);
            HttpClient.DefaultRequestHeaders.Add("User-Agent", _userAgent);
            HttpClient.DefaultRequestHeaders.Add("sec-ch-ua", _secChUa);
            HttpClient.DefaultRequestHeaders.Add("sec-ch-ua-mobile", _secChUaMobile);
            HttpClient.DefaultRequestHeaders.Add("Sec-Fetch-Mode", _secFetchMode);
            HttpClient.DefaultRequestHeaders.Add("sec-ch-ua-platform", _secChUaPlatform);
            HttpClient.Timeout = TimeSpan.FromSeconds(60);

        }


        public async Task<string> ConsultarBotIADosConsultas(string userText, Prompt promp)
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
                    ""history"": [
                            {
                                ""user"": ""{0}"",
                                ""system"": ""{3}""
                            }                            
                        ],
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
                                    ""selected_folders"": ""{4}"",
                                    ""follow_up_questions_prompt_content"": """",
                                    ""query_prompt_template"": """",
                                    ""selected_tags"": """"
                        }
                }";
                param = param.Replace("{0}", userText).Replace("{1}", PrompsRAG.SPLANTILLASYSTEMPERSONA)
                             .Replace("{2}", PrompsRAG.SPLANTILLAUSERPERSONA).Replace("{3}", promp.Promtsystem.Valor).Replace("{4}", promp.Folder.Valor);

                var data = new StringContent(JsonConvert.SerializeObject(JsonConvert.DeserializeObject<IAModel>(param)), Encoding.UTF8, "application/json");
                using (var httpResponse = await HttpClient.PostAsync(_url, data))
                {
                    var apiResponseString = await httpResponse.Content.ReadAsStringAsync();

                    if (httpResponse.StatusCode == HttpStatusCode.OK)
                    {

                        return apiResponseString.ToString();

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
                HttpClient.Dispose();
            }

            // free native resources if there are any.
            if (nativeResource != nint.Zero)
            {
                Marshal.FreeHGlobal(nativeResource);
                nativeResource = nint.Zero;
            }

            isDisposed = true;
        }

        public async Task<string> GetMessageRagAsync(string userText, Prompt promp)
        {

            var rag = await ConsultarBotIADosConsultas(userText, promp);
            if (!string.IsNullOrEmpty(rag))
            {
                var rgResp = JsonConvert.DeserializeObject<IAResponse>(rag);
                return rgResp.Answer;
            }
            return null;
        }


    }
}
