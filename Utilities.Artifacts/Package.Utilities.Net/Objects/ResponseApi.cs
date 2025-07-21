namespace Package.Utilities.Net
{
    using System.Globalization;
    using Microsoft.AspNetCore.Http;

    /// <summary>
    /// Clase que representa el objeto de respuesta de la api
    /// </summary>
    public readonly struct ResponseApi
    {
        /// <summary>
        /// Obtener el código interno del mensaje configurado
        /// </summary>
        public EnumerationException.Message Code { get; }

        /// <summary>
        /// Mensaje configurado
        /// </summary>
        public string Message { get; }

        /// <summary>
        /// Mensaje Interno configurado del response
        /// </summary>
        public DetailResponseApi InnerMessage { get; }


        /// <summary>
        /// Contructor que permite la construcción del objeto de retorno
        /// </summary>
        /// <param name="code">Obtener el código interno del mensaje configurado</param>
        /// <param name="message"> Mensaje configurado</param>
        /// <param name="innerMessage">Mensaje Interno configurado del response</param>
        public ResponseApi(EnumerationException.Message code, string message, DetailResponseApi innerMessage)
        {
            Code = code;
            Message = message;
            InnerMessage = innerMessage;
        }

        /// <summary>
        /// Contructor que permite la construcción del objeto de retorno
        /// </summary>
        /// <param name="message">Codigo del mensaje a retornar</param>
        /// <param name="tags">Tags de reemplazo en el Menssage</param>
        /// <param name="TypeMessage">Categorización del Error</param>
        public ResponseApi(EnumerationException.Message message,
                           string[] tags,
                           EnumerationApplication.TypeMessage TypeMessage)
        {
            Code = message;
            var approvedMessage = new TextResponseApi().GetText(message);
            InnerMessage = new DetailResponseApi(message, tags, TypeMessage, approvedMessage);
            Message = approvedMessage.Message;
            if (TypeMessage != EnumerationApplication.TypeMessage.Success && approvedMessage.NegativeMessage.IsValid())
            {
                Message = approvedMessage.NegativeMessage;
            }

            if (Message.IsValid() && tags.IsNotNull())
            {
                Message = string.Format(CultureInfo.CurrentCulture, Message, tags);
            }
        }

        /// <summary>
        /// Contructor que permite la construcción del objeto de retorno
        /// </summary>
        /// <param name="message">Codigo del mensaje a retornar</param>
        /// <param name="tags">Tags de reemplazo en el Mensage</param>
        /// <param name="detail">Texto de Un Detalle sobre el Mensage para su retorno</param>
        /// <param name="TypeMessage">Categorización del Error</param>
        public ResponseApi(EnumerationException.Message message,
                              string[] tags,
                              string detail,
                              EnumerationApplication.TypeMessage TypeMessage) :
            this(message, tags, TypeMessage)
        {
            InnerMessage.Detail = detail;
        }


        /// <summary>
        /// Categorización de StatusCode
        /// </summary>
        /// <param name="statusCodes"></param>
        /// <returns></returns>
        public static EnumerationApplication.TypeMessage GetTypeMessage(int statusCodes)
        {
            return statusCodes switch
            {
                StatusCodes.Status200OK => EnumerationApplication.TypeMessage.Success,
                StatusCodes.Status201Created => EnumerationApplication.TypeMessage.Success,
                StatusCodes.Status202Accepted => EnumerationApplication.TypeMessage.Success,
                StatusCodes.Status500InternalServerError => EnumerationApplication.TypeMessage.Error,
                StatusCodes.Status400BadRequest => EnumerationApplication.TypeMessage.Warning,
                _ => EnumerationApplication.TypeMessage.Alert
            };
        }
    }


    public class DetailResponseApi
    {
        public DetailResponseApi(EnumerationException.Message message,
                                 string[] tags,
                                 EnumerationApplication.TypeMessage typeMessage,
                                 TextResponse approvedMessage)
        {
            MessageTag = message.ToString();
            TypeMessage = typeMessage;
            TypeMessageTag = typeMessage.ToString();

            if (tags.IsNotNull())
            {
                Tags = tags;
            }

            Title = approvedMessage.Title;
            if (Title.IsValid())
            {
                TitleTag = $"Title{message}";
            }
        }

        /// <summary>
        /// Titulo configurado del mensaje
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Tag para mapear el Title en el cliente de retorno
        /// </summary>
        public string TitleTag { get; set; }

        /// <summary>
        /// Mensaje configurado
        /// </summary>
        public string Detail { get; set; }

        /// <summary>
        /// Tag para mapear el Message en el cliente de retorno
        /// </summary>
        public string MessageTag { get; set; }

        /// <summary>
        /// Tags de reemplazo en el Menssage
        /// </summary>
        public string[] Tags { get; }

        /// <summary>
        /// Categoria del Error
        /// </summary>
        public EnumerationApplication.TypeMessage TypeMessage { get; set; }

        /// <summary>
        /// Tag para mapear la TypeMessage en el cliente de retorno
        /// </summary>
        public string TypeMessageTag { get; }
    }


}
