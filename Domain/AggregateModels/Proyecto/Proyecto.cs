using Domain.AggregateModels.ValueObjects;
using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion del modelo agregado de dominio para la Entidad (Proyecto)
    /// </summary>
    public partial class Proyecto : Domain.Common.Entity 
    { 
       public Proyecto()
       { } 
       public Proyecto( string nombre,string tecnologias,string descripcion )
       { 
         Nombre = NombreValido.Create(nombre, ProyectoMetadata.Nombre).Value;
        Tecnologias = NombreValido.Create(tecnologias, ProyectoMetadata.Tecnologias).Value;
        Descripcion = NombreValido.Create(descripcion, ProyectoMetadata.Descripcion).Value;
       } 
       public NombreValido Nombre { get; private set; }

       public NombreValido Tecnologias { get; private set; }

       public NombreValido Descripcion { get; private set; }

     }
}
