namespace Package.Utilities.Net
{
    /// <summary>
    /// Clase que representa el objeto para parametrizar los listados de la api
    /// </summary>
    public class ParameterGetList
    {
        /// <summary>
        /// Inicialización de la Pagina
        /// </summary>
        private const int initPage = 1;

        /// <summary>
        /// Número de Pagina
        /// </summary>
        public int Page { get; set; } = initPage;

        /// <summary>
        /// Numero de Registros Por Pagina a Mostrar
        /// </summary>
        public int NumberRecords { get; set; } = Constants.DefaultNumeroDeRegistros;

        /// <summary>
        /// Columna por la que se ordenara
        /// </summary>
        public string OrderBy { get; set; }

        /// <summary>
        /// Dirección del Ordenamiento (Asc, Desc)
        /// </summary>
        public string DirecOrder { get; set; }
    }
}
