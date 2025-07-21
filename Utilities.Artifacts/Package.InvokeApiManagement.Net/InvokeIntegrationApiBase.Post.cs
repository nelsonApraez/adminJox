namespace Package.InvokeApiManagement.Net
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase base de encapsulamiento de la clase de cominicación para consumo de apis en ApiManagement.
    /// </summary>
    public abstract partial class InvokeIntegrationApiBase
    {
        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia)
            where T : class, new()
            where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<T, TR>(objectTransferencia);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="jsonMaping">Json Naming Policy</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, JsonNamingPolicy jsonMaping)
            where T : class, new()
            where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<T, TR>(objectTransferencia, jsonMaping);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<T, TR>(objectTransferencia, paramHeadersInvoke);
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
        public Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, JsonNamingPolicy jsonMaping, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<T, TR>(objectTransferencia, jsonMaping, paramHeadersInvoke);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Method/Parametro Api Url.</param>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia)
            where T : class, new()
            where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<T, TR>(paramUrlApi, objectTransferencia);
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
        public Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, JsonNamingPolicy jsonMaping)
            where T : class, new()
            where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<T, TR>(paramUrlApi, objectTransferencia, jsonMaping);
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
        public Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<T, TR>(paramUrlApi, objectTransferencia, paramHeadersInvoke);
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
        public Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, JsonNamingPolicy jsonMaping, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<T, TR>(paramUrlApi, objectTransferencia, jsonMaping, paramHeadersInvoke);
        }
    }
}
