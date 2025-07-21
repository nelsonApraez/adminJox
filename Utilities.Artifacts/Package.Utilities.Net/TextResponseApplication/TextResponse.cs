namespace Package.Utilities.Net
{
    using System;

    /// <summary>
    /// Objeto encargado de Orquestar los textos internos de kas respuestas de la api
    /// </summary>
    public readonly struct TextResponse : IEquatable<TextResponse>
    {
        /// <summary>
        /// Mesaje de retorno.
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Mensaje que se retorna en caso de un caso negativo de la acción.
        /// </summary>
        public string NegativeMessage { get; }

        /// <summary>
        /// Titulo del mensaje de retorno.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Contructor de llenado de las propiedades
        /// </summary>
        /// <param name="title">Titulo del mensaje de retorno</param>
        /// <param name="message">Mesaje de retorno</param>
        /// <param name="negativeMessage">Mensaje que se retorna en caso de un caso negativo de la acción</param>
        public TextResponse(string title, string message, string negativeMessage)
        {
            Message = message;
            NegativeMessage = negativeMessage;
            Title = title;
        }

        /// <summary>
        /// Contructor de llenado de las propiedades
        /// </summary>
        /// <param name="message">Mesaje de retorno</param>
        /// <param name="negativeMessage">Mensaje que se retorna en caso de un caso negativo de la acción</param>
        public TextResponse(string message, string negativeMessage) : this(null, message, negativeMessage) { }

        /// <summary>
        /// Contructor de llenado de las propiedades
        /// </summary>
        /// <param name="message">Mesaje de retorno</param>
        public TextResponse(string message) : this(null, message, null) { }

        /// <summary>
        /// Metodo de Comparacion de objetos TextResponse
        /// </summary>
        /// <param name="other">Objeto TextResponse a comparar</param>
        /// <returns>True/False si los objetos son iguales</returns>
        public bool Equals(TextResponse other)
        {
            return this.Message == other.Message &&
                    this.NegativeMessage == other.NegativeMessage &&
                    this.Title == other.Title;
        }
    }
}
