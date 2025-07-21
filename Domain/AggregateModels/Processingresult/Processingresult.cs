using Domain.AggregateModels.ValueObjects;
using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion del modelo agregado de dominio para la Entidad (Processingresult)
    /// </summary>
    public partial class Processingresult : Domain.Common.Entity
    { 
       public Processingresult()
       { } 
       public Processingresult( string proyectoid,string suggestedsolution,string benefitcalculation,string accompanyingstrategy )
       { 
         Proyectoid = System.Guid.Parse(proyectoid);
        Suggestedsolution = NombreValido.Create(suggestedsolution, ProcessingresultMetadata.Suggestedsolution).Value;
        Benefitcalculation = NombreValido.Create(benefitcalculation, ProcessingresultMetadata.Benefitcalculation).Value;
        Accompanyingstrategy = NombreValido.Create(accompanyingstrategy, ProcessingresultMetadata.Accompanyingstrategy).Value;
       } 
       public System.Guid Proyectoid { get; private set; }

       public NombreValido Suggestedsolution { get; private set; }

       public NombreValido Benefitcalculation { get; private set; }

       public NombreValido Accompanyingstrategy { get; private set; }

     }
}
