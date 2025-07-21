namespace Package.Utilities.Net
{
    using System.Collections.Generic;
    using static Package.Utilities.Net.EnumerationApplication;


    /// <summary>
    /// Clase que representa el objeto de filtro de las entidades para la api
    /// </summary>
    public struct Filter
    {
        public List<ItemsFilters> Filters
        {
            set; get;
        }
        public List<ItemSort> Sorts
        {
            set; get;
        }
    }

    /// <summary>
    /// Clase que representa la lista de objeto de Ordenamiento de las entidades para la api
    /// </summary>
    public struct ItemSort
    {
        /// <summary>
        /// Columna a Ordenamiento
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Dirección para ordenar la Columna a filtrar
        /// </summary>
        public string Direction { set; get; }
    }

    /// <summary>
    /// Clase que representa la lista de objeto filtro de las entidades para la api
    /// </summary>
    public class ItemsFilters
    {
        /// <summary>
        /// Columna a filtrar
        /// </summary>
        public string Name { set; get; }

        /// <summary>
        /// Valores de filtro
        /// </summary>
        public object[] Values { set; get; }

        /// <summary>
        /// Operación
        /// </summary>
        public OperationExpression Operator { set; get; }

        /// <summary>
        /// Condicionales
        /// </summary>
        public ConditionalExpression Conditional { set; get; }
    }
}
