using Domain.AggregateModels.ValueObjects;
using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion del modelo agregado de dominio para la Entidad (Respuesta)
    /// </summary>
    public partial class Respuesta : Domain.Common.Entity 
    { 
       public Respuesta()
       { } 
       public Respuesta(string proyectoid, string preguntaid,string valor )
       {
            Proyectoid = System.Guid.Parse(proyectoid);
            Preguntaid = System.Guid.Parse(preguntaid);
        Valor = NombreValido.Create(valor, RespuestaMetadata.Valor).Value;
       } 
       public System.Guid Preguntaid { get; private set; }

        public System.Guid Proyectoid { get; private set; }

        public NombreValido Valor { get; private set; }

     }
}
