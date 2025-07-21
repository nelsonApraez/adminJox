using System.ComponentModel;

namespace Package.Utilities.Net
{
    /// <summary>
    /// Enumeraciones de Configuraci�n de Excepciones de la aplicaci�n
    /// </summary>
    public class EnumerationException : EnumerationMessage
    {
        /// <summary>
        /// Enumeraci�n con los tipos de excepciones controlada
        /// </summary>
        public enum TypeCustomException { Undefined, Validation, BusinessException, NoContent, Unauthorized }

        public enum CategoryException
        {
            [Description("Generales")]
            General,
            [Description("Base de Datos")]
            DataBase,
            [Description("Negocio")]
            BusinessException,
            [Description("Validaciones")]
            Validation
        }
    }
}
