namespace Package.Utilities.Net
{
    using System;
    using System.Globalization;
    using System.Text.RegularExpressions;

    /// <summary>
    /// Clase con los metodos extendidos para centralizar funcionalidades puntuales de cada tipo de objeto
    /// </summary>
    [AttributeUsage(AttributeTargets.All)]
    public sealed class ValidatedNotNullAttribute : Attribute { }

    /// <summary>
    /// Attribute Extend para ocultar propiedades de los objetos en la exportacion a Excel
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public sealed class ExcelPropertyAttribute : Attribute
    {
        public string Name { private set; get; }
        public int Order { private set; get; }
        public bool Hidden { private set; get; }

        public ExcelPropertyAttribute(string name, int order = 0, bool hidden = false)
        {
            this.Name = name;
            this.Order = order;
            this.Hidden = hidden;
        }
    }

    /// <summary>
    /// Clase con los metodos extendidos para centralizar funcionalidades puntuales de cada tipo de objeto
    /// </summary>
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Calcular Dias, Horas y minutos apartir de minitos
        /// </summary>
        /// <param name="minutosTime">Tiempo en Minutos</param>
        /// <returns>Dias, Horas y minutos apartir de minutos de tiempo</returns>
        public static string[] CalculateTimeFromMinutes(this int minutosTime)
        {
            return CalculateTimeFromHours(minutosTime / 60d);
        }

        /// <summary>
        /// Calcular Dias, Horas y minutos apartir de horas
        /// </summary>
        /// <param name="hoursTime">Tiempo en Horas</param>
        /// <returns>Dias, Horas y minutos apartir de horas de tiempo</returns>
        public static string[] CalculateTimeFromHours(this double hoursTime)
        {
            int hours = (int)Math.Truncate(hoursTime);
            double decimales = hoursTime - hours;
            TimeSpan tiempo = new TimeSpan(
                                            hours,
                                            Convert.ToInt32(decimales * 60),
                                            0);

            return new string[] { tiempo.Days.PadLeftValue('0'), tiempo.Hours.PadLeftValue('0'), tiempo.Minutes.PadLeftValue('0') };
        }

        /// <summary>
        /// Adicionar parentesis a un texto
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string Parentesis(this string s)
        {
            // Check for empty string.  
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return string.Concat("(", s, ")");
        }

        /// <summary>
        /// Adicionar caracteres a la izquierda.
        /// </summary>
        /// <param name="number">Valor</param>
        /// <param name="padCaracter">Caracter a adicionar</param>
        /// <returns>Valor con caracteres adicionados</returns>
        public static string PadLeftValue(this int number, char padCaracter)
        {
            return number.PadLeftValue(padCaracter, 2);
        }

        /// <summary>
        /// Adicionar caracteres a la izquierda.
        /// </summary>
        /// <param name="number">Valor</param>
        /// <param name="padCaracter">Caracter a adicionar</param>
        /// <param name="totalValue">Tamaño del texto</param>
        /// <returns>Valor con caracteres adicionados</returns>
        public static string PadLeftValue(this int number, char padCaracter, int totalValue)
        {
            return Convert.ToString(number, CultureInfo.InvariantCulture).PadLeft(totalValue, padCaracter);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string FirstCharToUpper(this string s)
        {
            // Check for empty string.  
            if (string.IsNullOrEmpty(s))
            {
                return string.Empty;
            }

            return char.ToUpper(s[0], CultureInfo.CurrentCulture) + s[1..].ToLower(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        /// <returns></returns>
        public static string RemoveLineBreaks(this string lines) => lines.ReplaceLineBreaks(string.Empty);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="lines"></param>
        /// <param name="replacement"></param>
        /// <returns></returns>
        public static string ReplaceLineBreaks(this string lines, string replacement) => lines.Replace("\r\n", replacement).Replace("\r", replacement).Replace("\n", replacement);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="messageBusinessException"></param>
        public static void IsValidThrow([ValidatedNotNull] this string s, EnumerationMessage.Message messageBusinessException)
        {
            s.IsValidThrow(messageBusinessException, EnumerationException.TypeCustomException.BusinessException);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="messageBusinessException"></param>
        public static void IsValidThrow([ValidatedNotNull] this string s, EnumerationMessage.Message messageBusinessException,
                                                                          EnumerationException.TypeCustomException typeCustomException)
        {
            if (!s.IsValid())
            {
                throw new CustomException(typeCustomException, messageBusinessException);
            }
        }

        /// <summary>
        /// Se encarga de validar si un tipo fecha es diferente de nulo.
        /// </summary>
        /// <param name="dateTime">Tipo feccha</param>
        /// <returns>Verdadero o falso de acuerdo a la validación.</returns>
        public static bool IsNotNull(this DateTime? dateTime) => (dateTime != null && dateTime > DateTime.MinValue);

        /// <summary>
        /// BAD PATTERN: Pattern is null or blank, BAD PATTERN: Syntax error
        /// </summary>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsRegexPatternValid(this string testPattern)
        {
            bool isValid = true;
            if (testPattern.IsValid())
            {
                try
                {
                    _ = Regex.Match(string.Empty, testPattern);
                }
                catch (ArgumentException)
                {
                    isValid = false;
                }
            }
            else
            {
                isValid = false;
            }

            return (isValid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <param name="pattern"></param>
        /// <returns></returns>
        public static bool IsRegexPatternValid(this string text, string pattern)
        {
            bool isValid = false;
            if (pattern.IsRegexPatternValid() && text.IsValid())
            {
                Regex rx = new Regex(pattern, RegexOptions.Compiled | RegexOptions.IgnoreCase);
                return rx.IsMatch(text);
            }

            return (isValid);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValid([ValidatedNotNull] this string s) => (s.IsNotNull() && s.Trim().Length > 0);

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static bool IsValid([ValidatedNotNull] this int? s) => (s.HasValue && s.Value > 0);
    }
}
