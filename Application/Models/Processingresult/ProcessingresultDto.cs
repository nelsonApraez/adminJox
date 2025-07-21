namespace Application.Features.Models.Dto
{
    /// <summary>
    /// Esta clase representa la Implementacion DTO para la Entidad (Processingresult)
    /// </summary>
    public partial class ProcessingresultDto 
    { 
       public string Id { get; set; }

       public string Proyectoid { get; set; }

        /// <summary>
        /// Propiedad para obtener el campo Id de la tabla foranea Proyecto
        public ProyectoDto  ProyectoidNavigation  { get; set; } 

        /// </summary>
       public string Suggestedsolution { get; set; }

       public string Benefitcalculation { get; set; }

       public string Accompanyingstrategy { get; set; }

 
    }

}
