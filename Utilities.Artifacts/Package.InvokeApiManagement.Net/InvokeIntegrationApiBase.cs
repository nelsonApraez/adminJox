namespace Package.InvokeApiManagement.Net
{
    using System.Collections.Generic;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase base de encapsulamiento de la clase de cominicación para consumo de apis en ApiManagement.
    /// </summary>
    public abstract partial class InvokeIntegrationApiBase : IInvokeIntegrationApiManagement
    {
        /// <summary>
        /// Objeto InvokeIntegrationApiManagement
        /// </summary>
        private readonly IInvokeIntegrationApiManagement invokeIntegrationApiManagement;

        /// <summary>
        ///Constructor para inicializar IInvokeIntegrationApiManagement
        /// </summary>
        /// <param name="invokeIntegrationApiManagement"></param>
        protected InvokeIntegrationApiBase(IInvokeIntegrationApiManagement invokeIntegrationApiManagement)
        {
            this.invokeIntegrationApiManagement = invokeIntegrationApiManagement;
        }

        /// <summary>
        /// Modificar la api para la petición.
        /// </summary>
        /// <param name="urlApi">Url de la api a invocar</param>
        public void ApiBaseInvoke(string urlApi)
        {
            this.invokeIntegrationApiManagement.ApiBaseInvoke(urlApi);
        }

        /// <summary>
        /// Metodo para invocar el login para obtener el token para consumir ApiManagement
        /// </summary>
        /// <typeparam name="TR">Tipo de Objeto de Retorno</typeparam>
        /// <param name="objectTransferencia">Objeto de Retorno</param>
        /// <returns>Retorna el objeto de retorno de la respuesta de la api</returns>
        public Task<TR> InvokeLoginIntegrationAsync<TR>(List<KeyValuePair<string, string>> objectTransferencia) where TR : class, new()
        {
            return this.invokeIntegrationApiManagement.InvokeLoginIntegrationAsync<TR>(objectTransferencia);
        }
    }
}
