namespace Package.ProcessEngineApplication.Net.Handler.Interfaces
{
    /// <summary>
    /// Interfaz del Contenedor de manipulador de procesos
    /// </summary>
    public interface IProcessHandlerContainer
    {
        /// <summary>
        /// Metodo para obtener la referencias del Handler a Ejecutar
        /// </summary>
        /// <param name="reference">Handler que se ejecutara</param>
        /// <returns>Referencias del Handler a Ejecutar</returns>
        IProcessHandlerActivator GetHandler(string reference);
    }
}
