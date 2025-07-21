namespace Package.ProcessEngineApplication.Net.Dispatch.Interfaces
{
    using Package.ProcessEngineApplication.Net.Interfaces;

    /// <summary>
    /// Interfaz del Recopilador de solicitudes de proceso para gestion asincronica
    /// </summary>
    public interface IProcessRequestCollector
    {
        /// <summary>
        /// Metodo que recibe la peticion de proceso para encolarlas
        /// </summary>
        /// <param name="processRequestMessage">Mensaje con informacion para el handler</param>
        void Collect(IProcessRequestMessage processRequestMessage);
    }
}
