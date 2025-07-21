namespace Package.ProcessEngineApplication.Net.Dispatch
{
    using Package.ProcessEngineApplication.Net.Dispatch.Interfaces;
    using Package.ProcessEngineApplication.Net.Interfaces;
    using Package.Utilities.Net;
    using System.Collections.Generic;

    /// <summary>
    /// Clase del Recopilador de solicitudes de procesos para gestion asincronica
    /// </summary>
    public class ProcessRequestCollector : IProcessRequestCollector
    {
        /// <summary>
        /// Dummy de Procesos [Este se debe reemplazar por la estrategia correspondiente de colas]
        /// </summary>
        private readonly List<IProcessRequestMessage> lstProcessRequestMessage;

        /// <summary>
        /// Initializes a new instance of the ProcessRequestCollector
        /// </summary>
        public ProcessRequestCollector()
        {
            if (!lstProcessRequestMessage.IsNotNull())
            {
                lstProcessRequestMessage = new List<IProcessRequestMessage>();
            }
        }

        /// <summary>
        /// Metodo que recibe la peticion de proceso para encolarlas
        /// </summary>
        /// <param name="processRequestMessage">Mensaje con informacion para el handler</param>
        public void Collect(IProcessRequestMessage processRequestMessage)
        {
            lstProcessRequestMessage.Add(processRequestMessage);
        }
    }
}
