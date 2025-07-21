namespace Package.ProcessEngineApplication.Net
{
    using Package.ProcessEngineApplication.Net.Interfaces;
    using Package.Utilities.Net;
    using System;

    /// <summary>
    /// Clase [Contexto/Inicializcion] del Manejador de Mensaje de solicitud de proceso
    /// </summary>
    public class ProcessRequestMessage : IProcessRequestMessage
    {
        /// <summary>
        /// Gets el identificador del mensaje
        /// </summary>
        public string MessageId => Guid.NewGuid().ToString();

        /// <summary>
        /// Gets la referencia del mensaje que puede usarse para correlacionar
        /// </summary>
        public string Reference => HandlerId.GetEnumDescription();

        /// <summary>
        /// Gets or sets el identificador del handler que procesa el mensaje
        /// </summary>
        public EnumHandlerId HandlerId { get; set; }

        /// <summary>
        /// Gets or sets el contenido del mensaje
        /// </summary>
        public dynamic Content { get; set; }

        /// <summary>
        /// Gets or sets los errores
        /// </summary>
        public string Error { get; set; } = string.Empty;

        /// <summary>
        /// Gets or sets el contenido del mensaje
        /// </summary>
        public bool IsModoAsync { get; set; } = false;
    }
}
