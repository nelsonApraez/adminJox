namespace UnitTestsProject.Tests.Test.MockData.Entities
{
    using Domain.AggregateModels.ValueObjects;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Prompt]
    /// </summary>
    public static class PromptMockData
    {
        public static List<Domain.AggregateModels.Prompt> GetList()
        {
            return  new List<Domain.AggregateModels.Prompt>()
            {
                new Domain.AggregateModels.Prompt( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Nombre)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Promtuser)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Promtsystem)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Tags)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Folder)) ){ Id =  BaseBuilder.GetGuIdValue("1") },
                new Domain.AggregateModels.Prompt( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Nombre)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Promtuser)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Promtsystem)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Tags)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Folder)) ){ Id =  BaseBuilder.GetGuIdValue("2") },
                new Domain.AggregateModels.Prompt( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Nombre)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Promtuser)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Promtsystem)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Tags)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Prompt.Folder)) ){ Id =  BaseBuilder.GetGuIdValue("3") }
            };

        }

    }
}
