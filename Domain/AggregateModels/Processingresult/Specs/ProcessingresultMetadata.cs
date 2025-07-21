using Domain.Common;
namespace Domain.AggregateModels
{
    /// <summary>
    /// Esta clase representa la Implementacion la metadata para la Entidad (Processingresult)
    /// </summary>
    public static class ProcessingresultMetadata 
    { 
       public static Domain.Common.Metadata Suggestedsolution => new(nameof(Processingresult.Suggestedsolution), nameof(Processingresult.Suggestedsolution), 20000);
       public static Domain.Common.Metadata Benefitcalculation => new(nameof(Processingresult.Benefitcalculation), nameof(Processingresult.Benefitcalculation), 20000);
       public static Domain.Common.Metadata Accompanyingstrategy => new(nameof(Processingresult.Accompanyingstrategy), nameof(Processingresult.Accompanyingstrategy), 20000);
    }
}
