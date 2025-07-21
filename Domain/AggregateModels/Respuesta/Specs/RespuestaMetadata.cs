using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion la metadata para la Entidad (Respuesta)
    /// </summary>
    public static class RespuestaMetadata 
    { 
       public static Domain.Common.Metadata Valor => new(nameof(Respuesta.Valor), nameof(Respuesta.Valor),3000);
    }
}
