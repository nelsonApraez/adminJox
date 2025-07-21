namespace Domain.AggregateModels
{
    /// <summary>
    /// Objeto Extendido De La Entidad (Processingresult) Base y nos permite exponer un campo string de las tablas foraneas
    /// </summary>
    public partial class Processingresult
    {
        /// <summary>
        /// Propiedad para obtener el campo Id de la tabla foranea Proyecto
        /// </summary>
        public virtual Proyecto ProyectoidNavigation { get; private set;}

    }
}
