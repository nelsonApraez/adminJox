namespace Package.Utilities.Net
{
    /// <summary>
    /// Clase donde se consolidaran todos los valores fijos de configuraci�n de la aplicaci�n
    /// </summary>
    public static class Constants
    {
        /// <summary>
        /// Cantidad de Registros Por Defecto
        /// </summary>
        public static readonly int DefaultNumeroDeRegistros = 10;

        /// <summary>
        /// Tiempo de Caducidad de la MemoryCache el valor es en Horas
        /// </summary>
        public static readonly int TiempoDeCaducidadMemoryCache = 12;

        /// <summary>
        /// Nombre de la cache de "Connection" de la MemoryCache
        /// </summary>
        public static readonly string CacheConnectionMemoryCache = "AppConnection";

        /// <summary>
        /// Tag De Configuraci�n del Swagger en el AppSettings
        /// </summary>
        public static readonly string SwaggerConfiguration = "SwaggerConfiguration";

        /// <summary>
        /// Tag De Configuraci�n del Data Factory en el AppSettings 
        /// </summary>
        public static readonly string DataFactoryConfiguration = "ConfigurationDataFactory:";

        /// <summary>
        /// Tag De Configuraci�n del Swagger en el AppSettings
        /// </summary>
        public static readonly string ConfigurationApplication = "ConfigurationApplication:";

        /// <summary>
        /// Path base de login para api management
        /// </summary>
        public static readonly string PathLoginApiManagement = "/path/to/post/to";

        /// <summary>
        /// Path base de login para api management
        /// </summary>
        public static readonly string HeaderSubscriptionApiManagement = "Ocp-Apim-Subscription-Key";

        /// <summary>
        /// Content type de las peticiones
        /// </summary>
        public static readonly string ContentType = "application/json";

        /// <summary>
        /// Par�metro de aplicaci�n
        /// </summary>
        public static readonly string Autorization = "Authorization";

        /// <summary>
        /// Constante para la autenticaci�n tipo Bearer.
        /// </summary>
        public static readonly string Bearer = "Bearer";

        /// <summary>
        /// Constante para los Cors de la Api.
        /// </summary>
        public static readonly string CorsPolicy = "CorsPolicy";

        /// <summary>
        /// Par�metro de aplicaci�n
        /// </summary>
        public static readonly string ExampleDescription = "Ejemplo: 'Bearer eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng...'";

        /// <summary>
        /// Par�metro de aplicaci�n
        /// </summary>
        public static readonly string Oauth2 = "oauth2";

        /// <summary>
        /// Url del servicio health
        /// </summary>
        public static readonly string HealthUrl = "/health";

        /// <summary>
        /// Cadena para cadenas de caracteres maximo
        /// </summary>
        public static readonly string CharMaxLength = "caracteres maximo";

        /// <summary>
        /// Tag De Configuraci�n del modulo puesta a punto
        /// </summary>
        public static readonly string ModulePuestaPunto = "Puesta punto";


        public readonly struct DiscoveryTag
        {
            public static readonly string Aplicacion = "aplicacion";
            public static readonly string Modulo = "modulo";
            public static readonly string Funcionalidad = "funcionalidad";
            public static readonly string Analistaresponsable = "analistaresponsable";
            public static readonly string Eemailanalistaresponsable = "emailanalistaresponsable";
            public static readonly string Endpointgobierno = "endpointgobierno";
            public static readonly string Swagger = "swagger";
            public static readonly string Health = "health";
            public static readonly string Discovery = "discovery";
            public static readonly string Version = "version";
            public static readonly string Pordefecto = "pordefecto";
            public static readonly string Ultima = "ultima";
            public static readonly string V1 = "v1";
            public static readonly string V2 = "v2";
            public static readonly string Endpointsaplicacion = "endpointsaplicacion";
        }

        public readonly struct StaticMessage
        {
            public static readonly string NoObtuvoRespuestaExitosa = "No obtuvo una respuesta exitosa del api {0}: {1}.";
            public static readonly string LaValidacionDeOperacionNoEstaImplemantada = "La validaci�n de la {0} para la operaci�n {1} y la {2} no estan implementadas";
        }

        #region Valores por defecto de los parametros

        public struct DefaultParameters { }

        #endregion

        #region Codigo Errores SQL

        public readonly struct CodeSql
        {
            /// <summary>
            /// C�digo de Error SQL InsertarValoresNulos
            /// No se puede insertar el valor NULL en la columna '%1!', tabla '%2!'. La columna no admite valores NULL. Error de %3!.
            /// </summary>
            public static readonly int CodeInsertValuessNulll = 515;

            /// <summary>
            /// C�digo de Error SQL IdentityInsert
            /// No se puede insertar un valor expl�cito en la columna de identidad de la tabla '%1!' cuando IDENTITY_INSERT es OFF.
            /// </summary>
            public static readonly int CodeIdentityInsert = 544;

            /// <summary>
            /// C�digo de Error SQL ConflictoRestriccion
            /// Instrucci�n %1! en conflicto con la restricci�n %2! "%3!". El conflicto ha aparecido en la base de datos "%4!", tabla "%5!"%6!%7!%8!.
            /// </summary>
            public static readonly int CodeConflictRestriccion = 547;
            /// <summary>
            /// C�digo de Error SQL ClaveDuplicada
            /// No se puede insertar una fila de clave duplicada en el objeto '%1!' con �ndice �nico '%2!'. El valor de la clave duplicada es %3!.
            /// </summary>
            public static readonly int CodeUniquekey = 2601;
            /// <summary>
            /// C�digo de Error SQL RestriccionClaveDuplicada
            /// Infracci�n de la restricci�n %1! '%2!'. No se puede insertar una clave duplicada en el objeto '%3!'. El valor de la clave duplicada es %4!.
            /// </summary>
            public static readonly int CodeDuplicateKeyRestriction = 2627;
            /// <summary>
            /// C�digo de Error SQL ViolationOfMaxLengthstaticraint
            /// Los datos de cadena o binarios se truncar�an.
            /// </summary>
            public static readonly int CodeDataTruncated = 8152;
            /// <summary>
            /// C�digo de Error SQL ViolationOfMaxValuestaticraint
            /// </summary>
            public static readonly int CodeViolationOfMaxValueConstraint = 8115;
        }

        #endregion
    }
}
