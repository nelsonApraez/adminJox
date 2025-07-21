namespace Package.ProcessEngineApplication.Net
{
    using Package.ProcessEngineApplication.Net.Dispatch.Interfaces;
    using Package.ProcessEngineApplication.Net.Interfaces;

    /// <summary>
    /// Clase Contexto/Inicializción Fachada del motor de proceso
    /// </summary>
    public class ProcessEngineFacade : IProcessEngineFacade
    {
        /// <summary>
        /// Referencia a la instancia de la clase que encola los mensajes Asincrona
        /// </summary>
        private readonly IProcessRequestCollector processRequestCollector;

        /// <summary>
        /// Referencia a la instancia de la clase que envia los mensajes a los handlers Sincrona
        /// </summary>
        private readonly IProcessRequestDispatcher processRequestDispatcher;

        /// <summary>
        /// Initializes a new instance of the ProcessEngineFacade
        /// </summary>
        /// <param name="processRequestCollector">Instancia de la clase que encola los mensajes Asincrona</param>
        /// <param name="processRequestDispatcher">Instancia de la clase que envia los mensajes a los handlers Sincrona</param>
        public ProcessEngineFacade(IProcessRequestCollector processRequestCollector,
                                    IProcessRequestDispatcher processRequestDispatcher)
        {
            this.processRequestCollector = processRequestCollector;
            this.processRequestDispatcher = processRequestDispatcher;
        }

        /// <summary>
        /// Asignar tarea al manejador de proceso
        /// </summary>
        /// <param name="processRequestMessage">Mensaje con información para el handler</param>
        public void AssignTaskToProcessHandler(IProcessRequestMessage processRequestMessage)
        {
            if (processRequestMessage.IsModoAsync)
            {
                processRequestCollector.Collect(processRequestMessage);
            }
            else
            {
                try
                {
                    processRequestDispatcher.Dispatch(processRequestMessage);
                }
                catch (System.Exception ex)
                {
                    processRequestMessage.Error = ex.Message;
                    processRequestCollector.Collect(processRequestMessage);
                }
            }
        }
    }
}
