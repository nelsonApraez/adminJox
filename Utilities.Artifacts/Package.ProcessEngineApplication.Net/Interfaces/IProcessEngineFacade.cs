namespace Package.ProcessEngineApplication.Net.Interfaces
{
    /// <summary>
    /// Interface Fachada del motor de proceso
    /// </summary>
    public interface IProcessEngineFacade
    {
        /// <summary>
        /// Asignar tarea al manejador de proceso
        /// </summary>
        /// <param name="processRequestMessage">Mensaje con información para el handler</param>
        void AssignTaskToProcessHandler(IProcessRequestMessage processRequestMessage);
    }
}
