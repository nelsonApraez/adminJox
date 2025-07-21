namespace UnitTestsProject.Tests.Test.MockData.Entities
{
    using Domain.AggregateModels.ValueObjects;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Processingresult]
    /// </summary>
    public static class ProcessingresultMockData
    {
        public static List<Domain.AggregateModels.Processingresult> GetList()
        {
            return  new List<Domain.AggregateModels.Processingresult>()
            {
                new Domain.AggregateModels.Processingresult( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Proyectoid), true), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Suggestedsolution)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Benefitcalculation)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Accompanyingstrategy)) ){ Id =  BaseBuilder.GetGuIdValue("1") },
                new Domain.AggregateModels.Processingresult( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Proyectoid), true), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Suggestedsolution)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Benefitcalculation)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Accompanyingstrategy)) ){ Id =  BaseBuilder.GetGuIdValue("2") },
                new Domain.AggregateModels.Processingresult( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Proyectoid), true), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Suggestedsolution)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Benefitcalculation)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Processingresult.Accompanyingstrategy)) ){ Id =  BaseBuilder.GetGuIdValue("3") }
            };

        }

    }
}
