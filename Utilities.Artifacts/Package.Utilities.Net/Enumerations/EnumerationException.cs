using System.ComponentModel;

namespace Package.Utilities.Net
{
    /// <summary>
    /// Enumeraciones de Configuración de Excepciones de la aplicación
    /// </summary>
    public class EnumerationException : EnumerationMessage
    {
        /// <summary>
        /// Enumeración con los tipos de excepciones controlada
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
