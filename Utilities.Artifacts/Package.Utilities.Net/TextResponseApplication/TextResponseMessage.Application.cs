namespace Package.Utilities.Net
{
    using System.Collections.Generic;

    /// <summary>
    /// Clase encargada de Administar los textos de la aplicación de la forma definida por default
    /// </summary>
    internal abstract partial class TextResponseMessage
    {
        /// <summary>
        /// Mensajes configurado para la aplicación
        /// </summary>
        protected static Dictionary<EnumerationException.Message, TextResponse> TextResponseAplication { get; } =
            new Dictionary<EnumerationException.Message, TextResponse>
            {
                //Mensajes de Validaciones
            };
    }
}
