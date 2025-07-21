namespace Package.ProcessEngineApplication.Net
{
    using Package.ProcessEngineApplication.Net.Interfaces;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz del Manejador de procesos
    /// </summary>
    public interface IProcessHandlerActivator
    {

        /// <summary>
        /// Metodo de ejecuci�n de los handlers
        /// </summary>
        /// <param name="message">Mensaje con informaci�n para el handler</param>
        /// <returns></returns>
        Task<string> ExecuteProcess(IProcessRequestMessage message);
    }
}
