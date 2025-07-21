namespace Package.InvokeApiManagement.Net
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase base de encapsulamiento de la clase de cominicación para consumo de apis en ApiManagement.
    /// </summary>
    public abstract partial class InvokeIntegrationApiBase
    {
        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeIntegrationAsync<TR>() where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<TR>();
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeIntegrationAsync<TR>(Dictionary<string, string> paramHeadersInvoke) where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<TR>(paramHeadersInvoke);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Parametro Api Get.</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeIntegrationAsync<TR>(string paramUrlApi) where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<TR>(paramUrlApi);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Parametro Api Get.</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeIntegrationAsync<TR>(string paramUrlApi, Dictionary<string, string> paramHeadersInvoke) where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeIntegrationAsync<TR>(paramUrlApi, paramHeadersInvoke);
        }
    }
}
