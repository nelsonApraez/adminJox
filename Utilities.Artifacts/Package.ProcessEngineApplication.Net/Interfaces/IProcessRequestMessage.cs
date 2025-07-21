namespace Package.ProcessEngineApplication.Net.Interfaces
{
    /// <summary>
    /// Enumeracion de Tareas
    /// </summary>
    public enum EnumHandlerId
    {
        /// <summary>
        /// Handler Manejo de Logs
        /// </summary>
        LogHandling,

        /// <summary>
        /// Handler de Manejo de Auditoria Transaccional
        /// </summary>
        AuditManagement,

        /// <summary>
        /// Handler de Notificaciones
        /// </summary>
        Notification,

        /// <summary>
        /// Handler de trazas de la aplicaci�n
        /// </summary>
        Traces,

        /// <summary>
        /// Handler de seguridad de la aplicaci�n
        /// </summary>
        Security,

        /// <summary>
        /// Handler de data factory de la aplicaci�n
        /// </summary>
        DataExtraction,

        /// <summary>
        /// Handler de empresas
        /// </summary>
        CompaniesForUser,

        /// <summary>
        /// Handler de empresas
        /// </summary>
        ValidateActiveProfile
    }

    /// <summary>
    /// Clase y Extructura del Manejador de procesos
    /// </summary>
    public interface IProcessRequestMessage
    {
        /// <summary>
        /// Gets el identificador del mensaje
        /// </summary>
        string MessageId { get; }

        /// <summary>
        /// Gets la referencia del mensaje que puede usarse para correlacionar
        /// </summary>
        string Reference { get; }

        /// <summary>
        /// Gets or sets el identificador del handler que procesa el mensaje
        /// </summary>
        EnumHandlerId HandlerId { get; set; }

        /// <summary>
        /// Gets or sets el contenido del mensaje
        /// </summary>
        dynamic Content { get; set; }

        /// <summary>
        /// Gets or sets los errores
        /// </summary>
        string Error { get; set; }

        /// <summary>
        /// Gets or sets el contenido del mensaje
        /// </summary>
        bool IsModoAsync { get; set; }
    }
}
