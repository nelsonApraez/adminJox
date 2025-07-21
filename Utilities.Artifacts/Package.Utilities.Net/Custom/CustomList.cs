namespace Package.Utilities.Net
{
    using System.Collections.Generic;
    using System.Linq;

    /// <summary>
    /// Clase Customizada para la respuestas de listados paginados desde el servidor
    /// </summary>
    /// <typeparam name="T">Tipo Objeto [Entidad] De la Lista</typeparam>
    public class CustomList<T>
        where T : class
    {
        /// <summary>
        /// Constructor para inicializar la clase
        /// </summary>
        public CustomList()
        {

        }

        public CustomList(List<T> list)
        {
            this.List = list;
        }

        /// <summary>
        /// Constructor para inicializar la clase
        /// </summary>
        /// <param name="IQuery"></param>
        public CustomList(IQueryable<T> IQuery) { this.List = IQuery.ToList(); }

        /// <summary>
        /// Lista de los Objetos Obtenidos por el IQueryable de la [Entidad]
        /// </summary>
        public IEnumerable<T> List { get; set; }

        /// <summary>
        /// Objetos con los valores definidos de paginación
        /// </summary>
        public PagedList Paged { set; get; }
    }
}
