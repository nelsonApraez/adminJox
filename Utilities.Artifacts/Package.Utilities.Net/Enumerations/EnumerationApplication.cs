namespace Package.Utilities.Net
{
    /// <summary>
    /// Enumeraciones de Configuraci�n de la aplicaci�n
    /// </summary>
    public class EnumerationApplication
    {
        /// <summary>
        /// Enumeraci�n De los Items de Ordenamiento
        /// </summary>
        public enum Orden
        {
            Asc,
            Desc
        }

        /// <summary>
        /// Enumeraci�n De las Operaciones del Crud auditorias
        /// </summary>
        public enum Operations
        {
            Disable = 0,
            Create = 1,
            Update = 2,
            Remove = 3,
            Read = 4,
            Validate = 5
        }

        /// <summary>
        /// Enumeracion Para Validar implementaciones de los Items
        /// </summary>
        public enum Validations
        {
            Entity = 1,
            Button = 2
        }

        /// <summary>
        /// Enumeraci�n con las categorias de excepciones controlada
        /// </summary>
        public enum TypeMessage { Success = 200, Created = 201, Accepted = 202, Error = 500, Warning = 400, Unauthorized = 401, Alert = 100, Custom = 1000, NotAcceptable = 406 }

        /// <summary>
        /// Enumeraci�n para representar las Operaciones que se ejecutaran en los Expression Dinamicos.
        /// </summary>
        public enum OperationExpression
        {
            Equals,
            NotEquals,
            Minor,
            MinorEquals,
            Mayor,
            MayorEquals,
            Like,
            NotLike,
            StartsWith,
            NotStartsWith,
            EndsWith,
            NotEndsWith,
            Contains,
            Any
        }

        /// <summary>
        /// Enumeraci�n para representar los Condicionales que se ejecutaran en los Expression Dinamicos.
        /// </summary>
        public enum ConditionalExpression
        {
            And,
            Or
        }

        /// <summary>
        /// Enumeracion para configuracion de base 
        /// </summary>
        public enum AplicationEnums
        {
            DefaultConnectionSqlServer,
            DefaultConnectionMongo,
            DefaultConnectionStorage,
            ContenedorBlob,
            InstrumentationKey,
            ServiceBusConnection,
            PathSecurityApi,
            Entidad,
            Rol,
            Permiso,
            UsuarioEmpresa,
            PerfilUsuario,
            ObtenerEmpresasPorEmail,
            GetValidityByRolByEnterprise,
            codigoModeloEmpresa
        }
    }
}
