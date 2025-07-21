namespace Domain.AggregateModels
{
    /// <summary>
    /// Objeto Extendido De La Entidad (Respuesta) Base y nos permite exponer un campo string de las tablas foraneas
    /// </summary>
    public partial class Respuesta
    {
        /// <summary>
        /// Propiedad para obtener el campo Id de la tabla foranea Pregunta
        /// </summary>
        public virtual Pregunta PreguntaidNavigation { get; private set;}

        /// <summary>
        /// Propiedad para obtener el campo Id de la tabla foranea Proyecto
        /// </summary>
        public virtual Proyecto ProyectoidNavigation { get; private set; }

    }
}
