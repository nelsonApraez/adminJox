namespace Package.ProcessEngineApplication.Net.Dispatch
{
    using Package.ProcessEngineApplication.Net.Dispatch.Interfaces;
    using Package.ProcessEngineApplication.Net.Interfaces;

    /// <summary>
    /// Clase del Despachador de solicitudes de procesos sincronos
    /// </summary>
    public class ProcessRequestDispatcher : IProcessRequestDispatcher
    {
        /// <summary>
        /// Referencia al objeto que redirecciona los llamados a los handlers
        /// </summary>
        private readonly IProcessHandlerActivator processHandlerActivator;

        /// <summary>
        /// Initializes a new instance of the ProcessRequestDispatcher
        /// </summary>
        /// <param name="processHandlerActivator"><</param>
        public ProcessRequestDispatcher(IProcessHandlerActivator processHandlerActivator)
        {
            this.processHandlerActivator = processHandlerActivator;
        }

        /// <summary>
        /// Metodo que recibe la peticion de proceso para redireccionarla al handler adecuado para su procesamiento
        /// </summary>
        /// <param name="processRequestMessage">Mensaje con informacion para el handler</param>
        public void Dispatch(IProcessRequestMessage processRequestMessage)
        {
            this.processHandlerActivator.ExecuteProcess(processRequestMessage).ConfigureAwait(false);
        }
    }
}
