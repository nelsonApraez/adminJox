namespace Application.Features.Models.Dto
{
    /// <summary>
    /// Esta clase representa la Implementacion DTO para la Entidad (Prompt)
    /// </summary>
    public partial class PromptDto 
    { 
       public string Id { get; set; }

       public string Nombre { get; set; }

       public string Promtuser { get; set; }

       public string Promtsystem { get; set; }

       public string Tags { get; set; }

       public string Folder { get; set; }

 
    }

}
