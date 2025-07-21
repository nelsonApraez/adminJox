using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion la metadata para la Entidad (Proyecto)
    /// </summary>
    public static class ProyectoMetadata 
    { 
       public static Domain.Common.Metadata Nombre => new(nameof(Proyecto.Nombre), nameof(Proyecto.Nombre), 255);
       public static Domain.Common.Metadata Tecnologias => new(nameof(Proyecto.Tecnologias), nameof(Proyecto.Tecnologias), 255);
       public static Domain.Common.Metadata Descripcion => new(nameof(Proyecto.Descripcion), nameof(Proyecto.Descripcion), 255);
    }
}
