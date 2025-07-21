namespace Package.Utilities.Net
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Objeto para construccion de Querys Dinamicos con el IQueryable
    /// </summary>
    /// <typeparam name="T">Objeto</typeparam>
    public partial class ParameterOfList<T>
       where T : class
    {
        /// <summary>
        /// Página Actual
        /// </summary>
        private int Page = -1;

        /// <summary>
        /// Ordenamiento Dinamico por campo
        /// </summary>
        public Tuple<string, EnumerationApplication.Orden> OrderByDynamic { set; get; }

        /// <summary>
        /// Ordenamiento Dinamico por campo
        /// </summary>
        public Filter WhereDynamic { set; get; }

        /// <summary>
        /// Filtros Generico del Objeto
        /// </summary>
        public Expression<Func<T, bool>> Filter { set; get; }

        /// <summary>
        /// Include De tablas realacionales del Objeto
        /// </summary>
        public Expression<Func<T, object>>[] Include { set; get; }

        /// <summary>
        /// Ordenamiento Generico del Objeto
        /// </summary>
        public Func<IQueryable<T>, IOrderedQueryable<T>> OrderBy { set; get; }

        /// <summary>
        /// Numero de Registros por Pagina
        /// </summary>
        public int Take { set; get; }

        /// <summary>
        /// Numero de Registos en la Tabla
        /// </summary>
        public long MaxCount { set; get; }

        /// <summary>
        /// Calcula el numero de registros Hasta que se mostraran
        /// </summary>
        public int Skip => (Page > 0 && Take > 0) ? (Take * (Page - 1)) : Page;

        /// <summary>
        /// Numero de Paginas
        /// </summary>
        public long TotalPages => (MaxCount > 0 && Take > 0) ? (int)Math.Ceiling(MaxCount / (double)Take) : 0;

        /// <summary>
        /// Calcula el numero de registros Desde que se mostraran
        /// </summary>
        private long RecordsFrom => ((RecordsTo > 0 && Take > 0) ? (RecordsTo - Take) : 0);

        /// <summary>
        /// Calcula el numero de registros Hasta que se esta mostrando
        /// </summary>
        private long RecordsTo => ((Take > 0 && Page > 0) ? (Take * Page) : 0);

        /// <summary>
        /// Se Construye el objeto de Paginación
        /// </summary>
        public PagedList TextPag => new PagedList
        {
            Page = Page,
            PageSize = Take,
            TotalPages = TotalPages,
            MaxCount = MaxCount,

            RecordsFrom = RecordsFrom + 1,
            RecordsTo = (RecordsTo > MaxCount) ? MaxCount : RecordsTo
        };

        #region Configurar ParameterOfList

        /// <summary>
        /// Limpiar Objeto
        /// </summary>
        public void Clear()
        {
            MaxCount = 0;
            Page = -1;
            Take = -1;
            Filter = null;
            OrderBy = null;
            OrderByDynamic = new Tuple<string, EnumerationApplication.Orden>(string.Empty, EnumerationApplication.Orden.Asc);
            WhereDynamic = default;
            Include = null;
        }

        /// <summary>
        /// Adicionar filtros al objeto
        /// </summary>
        /// <param name="filter">Filtros Generico del Objeto</param>
        public void Add(Expression<Func<T, bool>> filter)
        {
            Filter = filter;
        }

        /// <summary>
        /// Adicionar filtros y ordenamientos al objeto
        /// </summary>
        /// <param name="filter">Filtros Generico del Objeto</param>
        /// <param name="orderBy">Ordenamiento Generico del Objeto</param>
        public void Add(Expression<Func<T, bool>> filter, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy)
        {
            Filter = filter;
            OrderBy = orderBy;
        }

        private void ConfigurateOrderByDynamic(string orderByDynamic, string DirecOrden)
        {
            if (DirecOrden.IsValid())
            {
                DirecOrden = DirecOrden.FirstCharToUpper();
            }

            if (!Enum.TryParse(DirecOrden, out EnumerationApplication.Orden eOrden))
            {
                eOrden = EnumerationApplication.Orden.Asc;
            }

            OrderByDynamic = new Tuple<string, EnumerationApplication.Orden>(orderByDynamic, eOrden);
        }

        #endregion
    }
}
