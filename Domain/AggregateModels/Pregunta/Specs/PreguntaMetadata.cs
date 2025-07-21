using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion la metadata para la Entidad (Pregunta)
    /// </summary>
    public static class PreguntaMetadata 
    { 
       public static Domain.Common.Metadata Valor => new(nameof(Pregunta.Valor), nameof(Pregunta.Valor), 255);
       public static Domain.Common.Metadata Descripcion => new(nameof(Pregunta.Descripcion), nameof(Pregunta.Descripcion), 255);
       public static Domain.Common.Metadata NombreCategoria => new(nameof(Pregunta.Descripcion), nameof(Pregunta.NombreCategoria), 100);
       public static Domain.Common.Metadata Categoria => new(nameof(Pregunta.Descripcion), nameof(Pregunta.NumeroCategoria), 10);
    }
}
