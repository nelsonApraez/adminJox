namespace Package.InvokeApiManagement.Net
{
    using Package.Utilities.Net;
    using System;
    using System.Collections.Generic;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Headers;
    using System.Text.Json;

    /// <summary>
    /// Clase de cominicación para consumo de apis en ApiManagement.
    /// </summary>
    public partial class InvokeIntegrationApiManagement : IInvokeIntegrationApiManagement
    {
        /// <summary>
        /// Contiene la url del api.
        /// </summary>
        private Uri apiInvoke;

        /// <summary>
        /// Contiene la llave de suscripción.
        /// </summary>
        private readonly Dictionary<string, string> headersInvoke = new Dictionary<string, string>();

        /// <summary>
        /// Constructor para inicializar las variables
        /// </summary>
        /// <param name="urlApi"></param>
        /// <param name="subscriptionKey"></param>
        public InvokeIntegrationApiManagement(Uri urlApi, string subscriptionKey)
        {
            apiInvoke = urlApi;
            if (subscriptionKey.IsValid())
            {
                headersInvoke.Add(Constants.HeaderSubscriptionApiManagement, subscriptionKey);
            }
        }

        /// <summary>
        /// Se encarga de setear la api para la petición.
        /// </summary>
        /// <param name="urlApi">Url del api.</param>
        public void ApiBaseInvoke(string urlApi) { this.apiInvoke = new Uri(urlApi); }

        /// <summary>
        /// Metodo para invocar el login para obtener el token para consumir ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async System.Threading.Tasks.Task<TR> InvokeLoginIntegrationAsync<TR>(List<KeyValuePair<string, string>> objectTransferencia)
            where TR : class, new()
        {
            return await InvokeApiAsync<TR>(null, async (httpClient) =>
            {
                var request = new HttpRequestMessage(HttpMethod.Post, Constants.PathLoginApiManagement)
                {
                    Content = new FormUrlEncodedContent(objectTransferencia)
                };

                return await httpClient.PostAsync(apiInvoke, request.Content).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }

        /// <summary>
        /// Adiciona Las cabeceras a la petición.
        /// </summary>
        /// <param name="httpCliente">Instanca creada del http client para la petición</param>
        private void AddHeader(HttpClient httpCliente, Dictionary<string, string> paramHeadersInvoke)
        {
            if (headersInvoke.IsNotNull())
            {
                foreach (var item in headersInvoke)
                {
                    httpCliente.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            if (paramHeadersInvoke.IsNotNull())
            {
                foreach (var item in paramHeadersInvoke)
                {
                    httpCliente.DefaultRequestHeaders.Add(item.Key, item.Value);
                }
            }

            httpCliente.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(Constants.ContentType));
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR"></typeparam>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <param name="funcInvoke">Delegate para ejecutar la petición</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        private async System.Threading.Tasks.Task<TR> InvokeApiAsync<TR>(Dictionary<string, string> paramHeadersInvoke, Func<HttpClient, System.Threading.Tasks.Task<HttpResponseMessage>> funcInvoke)
            where TR : class, new()
        {
            TR objResponseApi = null;
            ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls12;

            using (var httpCliente = new HttpClient())
            {
                AddHeader(httpCliente, paramHeadersInvoke);

                var response = await funcInvoke(httpCliente).ConfigureAwait(false);
                objResponseApi = await ResponseApi<TR>(response).ConfigureAwait(false);
            }

            return objResponseApi;
        }

        /// <summary>
        /// Procesa la peticion realizada para devolver el resultado
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="response">Response de la petición realizada</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        private async System.Threading.Tasks.Task<TR> ResponseApi<TR>(HttpResponseMessage response)
            where TR : class, new()
        {
            if (response.IsSuccessStatusCode)
            {
                return (await response.Content.ReadAsStringAsync().ConfigureAwait(false)).ToJsonDeserialize<TR>(new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                });
            }
            else
            {
                throw new CustomException(string.Format(Constants.StaticMessage.NoObtuvoRespuestaExitosa, apiInvoke,
                await response.Content.ReadAsStringAsync().ConfigureAwait(false)));
            }
        }
    }
}
