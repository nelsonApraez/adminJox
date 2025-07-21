namespace UnitTestsProject.Tests.Test.MockData.Dtos
{
    using System;
    using System.Collections.Generic;
    using Application.Features.Models.Dto;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Processingresult]
    /// </summary>
    public static class ProcessingresultDtoMockData
    {
        public static List<ProcessingresultDto> GetList()
        {
            return new List<ProcessingresultDto>()
            {
                new ProcessingresultDto(){ Id =  BaseBuilder.GetGuIdValue("1").ToString(), 
                    Proyectoid = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Proyectoid), true), 
                    Suggestedsolution = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Suggestedsolution)), 
                    Benefitcalculation = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Benefitcalculation)), 
                    Accompanyingstrategy = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Accompanyingstrategy)) },
                new ProcessingresultDto(){ Id =  BaseBuilder.GetGuIdValue("2").ToString(), 
                    Proyectoid = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Proyectoid), true), 
                    Suggestedsolution = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Suggestedsolution)), 
                    Benefitcalculation = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Benefitcalculation)), 
                    Accompanyingstrategy = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Accompanyingstrategy)) },
                new ProcessingresultDto(){ Id =  BaseBuilder.GetGuIdValue("3").ToString(), 
                    Proyectoid = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Proyectoid), true), 
                    Suggestedsolution = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Suggestedsolution)), 
                    Benefitcalculation = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Benefitcalculation)), 
                    Accompanyingstrategy = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Processingresult.Accompanyingstrategy)) },
            };

        }

       public static ProcessingresultDto BuildProcessingresultDto()
       {
          return GetList()[0]; 
       }

       public static ProcessingresultDto BuildProcessingresultDto(string id, string proyectoid, string suggestedsolution, string benefitcalculation, string accompanyingstrategy)
       {
          return new MockDataBuilder<ProcessingresultDto>()
                    .With(x => x.Id,id)
                     .With(x => x.Proyectoid,proyectoid)
                     .With(x => x.Suggestedsolution,suggestedsolution)
                     .With(x => x.Benefitcalculation,benefitcalculation)
                     .With(x => x.Accompanyingstrategy,accompanyingstrategy)
                     .Build();
       }

   }
}
