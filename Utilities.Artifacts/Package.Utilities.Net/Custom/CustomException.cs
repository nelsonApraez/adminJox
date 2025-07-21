namespace Package.Utilities.Net
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.Serialization;
    using static Package.Utilities.Net.EnumerationException;


    /// <summary>
    /// Customizacion de las Excepciones del Proyecto
    /// </summary>
    [Serializable]
    public class CustomException : Exception
    {
        /// <summary>
        /// Propiedad para Obtener el Usuario de la excepción controlada
        /// </summary>
        public string Username { get; private set; }

        /// <summary>
        /// Propiedad para Obtener el nombre del recurso donde se presento la excepción controlada
        /// </summary>
        public string ResourceName { get; private set; }

        /// <summary>
        /// Propiedad asignar un codigo GUID a la Exception
        /// </summary>
        public Guid GuidException { get; private set; }

        /// <summary>
        /// Propiedad para Obtener el Validation Errors de la excepción controlada
        /// </summary>
        public IList<string> ValidationErrors { get; private set; }

        /// <summary>
        /// Propiedad para obtener el mensaje de la validación de negocio.
        /// </summary>
        public string ErrorMessageThrowBusinessValidation => new TextResponseApi().GetTextResponseMessage(ErrorBusiness).Message;

        /// <summary>
        /// Propiedad para Obtener el tipo de excepción controlada
        /// </summary>
        private TypeCustomException typeException = TypeCustomException.Undefined;
        public TypeCustomException TypeException { private set { typeException = value; } get { return typeException; } }

        /// <summary>
        /// Propiedad Para Obtener un mensaje de negocio controlado
        /// </summary>
        private EnumerationException.Message errorBusiness = EnumerationMessage.Message.ErrorGeneral;
        public EnumerationException.Message ErrorBusiness { private set { errorBusiness = value; } get { return errorBusiness; } }

        /// <summary>
        /// Propiedad para Obtener los Valores para reemplazar en los textos de la excepción controlada "ErrorBusiness"
        /// </summary>
        public string[] TagTextBusiness { private set; get; }

        /// <summary>
        /// Propiedad para controlar el estado de visualizacion de los componentes
        /// </summary>
        public bool ReadOnly { private set; get; }

        /// <summary>
        /// Configuración por Defecto
        /// </summary>
        private void DefaultValues()
        {
            TagTextBusiness = null;
            ValidationErrors = null;
            Username = string.Empty;
            ResourceName = string.Empty;
            GuidException = Guid.NewGuid();
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        public CustomException() { DefaultValues(); }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        protected CustomException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            DefaultValues();
            Username = info.GetString("Username");
            ResourceName = info.GetString("ResourceName");
            ValidationErrors = (IList<string>)info.GetValue("ValidationErrors", typeof(IList<string>));
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="message"></param>
        public CustomException(string message) : base(message) { DefaultValues(); }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="resourceName"></param>
        /// <param name="validationErrors"></param>
        public CustomException(string message, string resourceName, IList<string> validationErrors) : this(message)
        {
            ResourceName = resourceName;
            ValidationErrors = validationErrors;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="username"></param>
        /// <param name="resourceName"></param>
        /// <param name="validationErrors"></param>
        public CustomException(string message, string username, string resourceName, IList<string> validationErrors) : this(message, resourceName, validationErrors)
        {
            Username = username;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException"></param>
        public CustomException(string message, Exception innerException) : base(message, innerException) { DefaultValues(); }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="ex">Excepcion Generada para la transformación</param>
        public CustomException(Exception ex) : base(ex?.Message, ex) { DefaultValues(); }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="typeException">Tipo de excepción controlada</param>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness) : this()
        {
            ErrorBusiness = errorBusiness;
            TypeException = typeException;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="typeException">Tipo de excepción controlada</param>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        /// <param name="ex">Excepcion Generada para la transformación</param>
        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness, Exception ex) : this(ex)
        {
            ErrorBusiness = errorBusiness;
            TypeException = typeException;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="typeException">Tipo de excepción controlada</param>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        /// <param name="tagTextBusiness">Valores para reemplazar en los textos de la excepción controlada</param>
        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness, string[] tagTextBusiness) : this(typeException, errorBusiness)
        {
            TagTextBusiness = tagTextBusiness;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="typeException">Tipo de excepción controlada</param>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        /// <param name="tagTextBusiness">Valores para reemplazar en los textos de la excepción controlada</param>
        /// <param name="ex">Excepcion Generada para la transformación</param>
        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness, string[] tagTextBusiness, Exception ex) : this(typeException, errorBusiness, ex)
        {
            TagTextBusiness = tagTextBusiness;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción. 
        /// </summary>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        public CustomException(EnumerationException.Message errorBusiness) : this()
        {
            ErrorBusiness = errorBusiness;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción. 
        /// </summary>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        /// <param name="ex">Excepcion Generada para la transformación</param>
        public CustomException(EnumerationException.Message errorBusiness, Exception ex) : this(ex)
        {
            ErrorBusiness = errorBusiness;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción. 
        /// </summary>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        /// <param name="tagTextBusiness">Valores para reemplazar en los textos de la excepción controlada</param>
        public CustomException(EnumerationException.Message errorBusiness, string[] tagTextBusiness) : this(errorBusiness)
        {
            TagTextBusiness = tagTextBusiness;
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        /// <param name="tagTextBusiness">Valores para reemplazar en los textos de la excepción controlada</param>
        /// <param name="ex">Excepcion Generada para la transformación</param>
        public CustomException(EnumerationException.Message errorBusiness, string[] tagTextBusiness, Exception ex) : this(errorBusiness, ex)
        {
            TagTextBusiness = tagTextBusiness;
        }

        public CustomException(EnumerationException.Message errorBusiness, Guid guidException, string[] tagTextBusiness, Exception ex) : this(errorBusiness, ex)
        {
            TagTextBusiness = tagTextBusiness;
            GuidException = guidException;
        }

        /// <summary>
        /// Se sobre escribe el metodo de Object Data
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public override void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            if (info == null)
            {
                throw new ArgumentNullException(nameof(info));
            }

            info.AddValue("Username", Username);
            info.AddValue("ResourceName", ResourceName);
            info.AddValue("ValidationErrors", ValidationErrors, typeof(IList<string>));

            base.GetObjectData(info, context);
        }

        /// <summary>
        /// Sobrecarga del Contructor para la excepción.
        /// </summary>
        /// <param name="typeException">Tipo de excepción controlada</param>
        /// <param name="errorBusiness">Mensaje de negocio controlado</param>
        /// <param name="readOnly">Indica si debe ser modo solo Lectura</param>
        /// <param name="ex">Excepcion Generada para la transformación</param>
        public CustomException(TypeCustomException typeException, EnumerationException.Message errorBusiness, bool readOnly, Exception ex) : this(errorBusiness, ex)
        {
            ReadOnly = readOnly;
        }

    }
}
