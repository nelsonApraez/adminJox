namespace Package.Utilities.Net
{
    /// <summary>
    /// Clase que representa el objeto con los valores de paginacion para retornar la api
    /// </summary>
    public class PagedList
    {
        /// <summary>
        /// P�gina Actual
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Numero de Paginas Totales
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// N�mero Total de P�ginas
        /// </summary>
        public long TotalPages { get; set; }

        /// <summary>
        /// Total de Registros
        /// </summary>
        public long MaxCount { get; set; }

        /// <summary>
        /// N�mero de Registros Desde
        /// </summary>
        public long RecordsFrom { get; set; }

        /// <summary>
        /// N�mero de Registros Hasta
        /// </summary>
        public long RecordsTo { get; set; }
    }
}
