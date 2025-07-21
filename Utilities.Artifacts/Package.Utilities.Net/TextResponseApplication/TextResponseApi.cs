namespace Package.Utilities.Net
{
    /// <summary>
    /// Clase encargada de Orquestar los textos de respuesta de la api
    /// </summary>
    internal partial class TextResponseApi : TextResponseMessage
    {
        /// <summary>
        /// Obtener mensaje para retornar en las apis
        /// </summary>
        /// <param name="cusMessage">Mensaje que se Retornara</param>
        /// <returns>Objeto TextResponse con las propiedades cargadas del mensaje configurado</returns>
        internal TextResponse GetText(EnumerationException.Message cusMessage)
        {
            return GetTextResponseMessage(cusMessage);
        }


        /// <summary>
        /// Retirna el mensaje customizado de procesamiento de entidades de negocio
        /// </summary>
        /// <param name="cusMessage"></param>
        /// <returns></returns>
        internal string GetTextCustom(string cusMessage)
        {
            return GetTextResourceMessage(cusMessage);
        }
    }
}
