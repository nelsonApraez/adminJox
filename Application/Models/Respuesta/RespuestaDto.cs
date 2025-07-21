namespace Application.Features.Models.Dto
{
    /// <summary>
    /// Esta clase representa la Implementacion DTO para la Entidad (Respuesta)
    /// </summary>
    public partial class RespuestaDto 
    { 
       public string Id { get; set; }

       public string Preguntaid { get; set; }

        public string Proyectoid { get; set; }

        /// </summary>
        public string Valor { get; set; }

        /// <summary>
        /// Propiedad para obtener el campo Id de la tabla foranea Pregunta
        public PreguntaDto PreguntaidNavigation { get; set; }

        /// <summary>
        /// Propiedad para obtener el campo Id de la tabla foranea Pregunta
        public ProyectoDto ProyectoidNavigation { get; set; }
    }

}
