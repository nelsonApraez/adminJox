namespace Package.InvokeApiManagement.Net
{
    using System.Collections.Generic;
    using System.Text.Json;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz de cominicación para consumo de apis en ApiManagement
    /// </summary>
    public interface IInvokeIntegrationApiManagement
    {
        /// <summary>
        /// Modificar la api para la petición.
        /// </summary>
        /// <param name="urlApi">Url de la api a invocar</param>
        void ApiBaseInvoke(string urlApi);

        /// <summary>
        /// Metodo para invocar el login para obtener el token para consumir ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeLoginIntegrationAsync<TR>(List<KeyValuePair<string, string>> objectTransferencia)
            where TR : class, new();

        #region Requests Post

        /// <summary>
        /// Implementacion para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia)
            where T : class, new()
            where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Method/Parametro Api Url.</param>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia)
            where T : class, new()
            where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="jsonMaping">Json Naming Policy</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, JsonNamingPolicy jsonMaping)
            where T : class, new()
            where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Method/Parametro Api Url.</param>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="jsonMaping">Json Naming Policy</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, JsonNamingPolicy jsonMaping)
            where T : class, new()
            where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Method/Parametro Api Url.</param>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="T">Tipo de Objeto de Transferencia</typeparam>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <param name="jsonMaping">Json Naming Policy</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<T, TR>(T objectTransferencia, JsonNamingPolicy jsonMaping, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new();

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
        Task<TR> InvokeIntegrationAsync<T, TR>(string paramUrlApi, T objectTransferencia, JsonNamingPolicy jsonMaping, Dictionary<string, string> paramHeadersInvoke)
            where T : class, new()
            where TR : class, new();

        #endregion

        #region Requests Get

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<TR>() where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Parametro Api Get.</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<TR>(string paramUrlApi) where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<TR>(Dictionary<string, string> paramHeadersInvoke)
            where TR : class, new();

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Parametro Api Get.</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        Task<TR> InvokeIntegrationAsync<TR>(string paramUrlApi, Dictionary<string, string> paramHeadersInvoke)
            where TR : class, new();

        #endregion
    }
}
