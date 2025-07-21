using Domain.AggregateModels.ValueObjects;
using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion del modelo agregado de dominio para la Entidad (Prompt)
    /// </summary>
    public partial class Prompt : Domain.Common.Entity
    { 
       public Prompt()
       { } 
       public Prompt( string nombre,string promtuser,string promtsystem,string tags,string folder )
       { 
         Nombre = NombreValido.Create(nombre, PromptMetadata.Nombre).Value;
        Promtuser = NombreValido.Create(promtuser, PromptMetadata.Promtuser).Value;
        Promtsystem = NombreValido.Create(promtsystem, PromptMetadata.Promtsystem).Value;
        Tags = NombreValido.Create(tags, PromptMetadata.Tags).Value;
        Folder = NombreValido.Create(folder, PromptMetadata.Folder).Value;
       } 
       public NombreValido Nombre { get; private set; }

       public NombreValido Promtuser { get; private set; }

       public NombreValido Promtsystem { get; private set; }

       public NombreValido Tags { get; private set; }

       public NombreValido Folder { get; private set; }

     }
}
