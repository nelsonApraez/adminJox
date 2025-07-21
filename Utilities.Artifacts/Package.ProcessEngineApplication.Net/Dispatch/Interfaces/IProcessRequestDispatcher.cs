namespace Package.ProcessEngineApplication.Net.Dispatch.Interfaces
{
    using Package.ProcessEngineApplication.Net.Interfaces;

    /// <summary>
    /// Interfaz del Despachador de solicitudes de proceso sincrono
    /// </summary>
    public interface IProcessRequestDispatcher
    {
        /// <summary>
        /// Metodo que recibe la peticion de proceso para redireccionarla al handler adecuado para su procesamiento
        /// </summary>
        /// <param name="processRequestMessage">Mensaje con informacion para el handler</param>
        void Dispatch(IProcessRequestMessage processRequestMessage);
    }
}
