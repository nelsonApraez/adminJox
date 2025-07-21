using Domain.AggregateModels.ValueObjects;
using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion del modelo agregado de dominio para la Entidad (Pregunta)
    /// </summary>
    public partial class Pregunta : Domain.Common.Entity
    {
        public Pregunta()
        { }
        public Pregunta(string valor, string descripcion, int numeroCategoria, string nombreCategoria)
        {
            Valor = NombreValido.Create(valor, PreguntaMetadata.Valor).Value;
            Descripcion = NombreValido.Create(descripcion, PreguntaMetadata.Descripcion).Value;
            NumeroCategoria = numeroCategoria;
            NombreCategoria = NombreValido.Create(nombreCategoria, PreguntaMetadata.NombreCategoria).Value;
        }

        public NombreValido Valor { get; private set; }

        public NombreValido Descripcion { get; private set; }

        public int NumeroCategoria { get; private set; }

        public NombreValido NombreCategoria { get; private set; }
    }
}
