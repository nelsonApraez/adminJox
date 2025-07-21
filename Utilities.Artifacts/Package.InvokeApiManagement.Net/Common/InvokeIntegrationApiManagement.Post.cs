namespace Package.InvokeApiManagement.Net
{
    using Package.Utilities.Net;
    using System;
    using System.Collections.Generic;
    using System.Net.Http;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase de cominicación para consumo de apis en ApiManagement.
    /// </summary>
    public partial class InvokeIntegrationApiManagement
    {
        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia)
            where T : class, new()
            where TR : class, new()
        {
            return await InvokeIntegrationAsync<T, TR>(objectTransferencia, JsonNamingPolicy.CamelCase, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Method/Parametro Api Url.</param>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia)
            where T : class, new()
            where TR : class, new()
        {
            return await InvokeIntegrationAsync<T, TR>(paramUrlApi, objectTransferencia, JsonNamingPolicy.CamelCase, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="jsonMaping">Json Naming Policy</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, JsonNamingPolicy jsonMaping)
            where T : class, new()
            where TR : class, new()
        {
            return await InvokeIntegrationAsync<T, TR>(objectTransferencia, jsonMaping, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Method/Parametro Api Url.</param>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="jsonMaping">Json Naming Policy</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, JsonNamingPolicy jsonMaping)
            where T : class, new()
            where TR : class, new()
        {
            return await InvokeIntegrationAsync<T, TR>(paramUrlApi, objectTransferencia, jsonMaping, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new()
        {
            return await InvokeIntegrationAsync<T, TR>(objectTransferencia, JsonNamingPolicy.CamelCase, paramHeadersInvoke).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Method/Parametro Api Url.</param>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new()
        {
            return await InvokeIntegrationAsync<T, TR>(paramUrlApi, objectTransferencia, JsonNamingPolicy.CamelCase, paramHeadersInvoke).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="jsonMaping">Json Naming Policy</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, JsonNamingPolicy jsonMaping, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new()
        {
            return await InvokeIntegrationAsync<T, TR>(string.Empty, objectTransferencia, jsonMaping, paramHeadersInvoke).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Method/Parametro Api Url.</param>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="jsonMaping">Json Naming Policy</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, JsonNamingPolicy jsonMaping, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new()
        {
            return await InvokeApiAsync<TR>(paramHeadersInvoke, async (httpCliente) =>
            {
                var httpPeticion = new StringContent(objectTransferencia.ToJsonSerialize(new JsonSerializerOptions
                {
                    IgnoreNullValues = true,
                    PropertyNameCaseInsensitive = true,
                    PropertyNamingPolicy = jsonMaping
                }), Encoding.UTF8, Constants.ContentType);

                return await httpCliente.PostAsync(paramUrlApi.IsValid() ?
                                                   new Uri($"{apiInvoke}/{paramUrlApi}") :
                                                   apiInvoke, httpPeticion).ConfigureAwait(false);
            }).ConfigureAwait(false);
        }
    }
}
