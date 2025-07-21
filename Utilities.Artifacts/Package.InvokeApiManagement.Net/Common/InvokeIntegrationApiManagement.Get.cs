namespace Package.InvokeApiManagement.Net
{
    using Package.Utilities.Net;
    using System;
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase de cominicación para consumo de apis en ApiManagement.
    /// </summary>
    public partial class InvokeIntegrationApiManagement
    {
        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<TR>() where TR : class, new()
        {
            return await InvokeIntegrationAsync<TR>(string.Empty, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Parametro Api Get.</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<TR>(string paramUrlApi) where TR : class, new()
        {
            return await InvokeIntegrationAsync<TR>(paramUrlApi, null).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<TR>(Dictionary<string, string> paramHeadersInvoke)
            where TR : class, new()
        {
            return await InvokeIntegrationAsync<TR>(string.Empty, paramHeadersInvoke).ConfigureAwait(false);
        }

        /// <summary>
        /// Metodo para invocar metodos de ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="paramUrlApi">Parametro Api Get.</param>
        /// <param name="paramHeadersInvoke">Cabeceras de la Peticion Headers</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public async Task<TR> InvokeIntegrationAsync<TR>(string paramUrlApi, Dictionary<string, string> paramHeadersInvoke)
            where TR : class, new()
        {
            return await InvokeApiAsync<TR>(paramHeadersInvoke, async (httpCliente) =>
                                                await httpCliente.GetAsync(paramUrlApi.IsValid() ?
                                                                           new Uri($"{apiInvoke}/{paramUrlApi}") :
                                                                           apiInvoke).ConfigureAwait(false)
                                            ).ConfigureAwait(false);
        }
    }
}
