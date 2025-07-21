namespace UnitTestsProject.Tests.Test.MockData.Dtos
{
    using System;
    using System.Collections.Generic;
    using Application.Features.Models.Dto;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Prompt]
    /// </summary>
    public static class PromptDtoMockData
    {
        public static List<PromptDto> GetList()
        {
            return new List<PromptDto>()
            {
                new PromptDto(){ Id =  BaseBuilder.GetGuIdValue("1").ToString(), 
                    Nombre = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Nombre)), 
                    Promtuser = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Promtuser)), 
                    Promtsystem = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Promtsystem)), 
                    Tags = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Tags)), 
                    Folder = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Folder)) },
                new PromptDto(){ Id =  BaseBuilder.GetGuIdValue("2").ToString(), 
                    Nombre = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Nombre)), 
                    Promtuser = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Promtuser)), 
                    Promtsystem = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Promtsystem)), 
                    Tags = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Tags)), 
                    Folder = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Folder)) },
                new PromptDto(){ Id =  BaseBuilder.GetGuIdValue("3").ToString(), 
                    Nombre = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Nombre)), 
                    Promtuser = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Promtuser)), 
                    Promtsystem = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Promtsystem)), 
                    Tags = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Tags)), 
                    Folder = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Prompt.Folder)) },
            };

        }

       public static PromptDto BuildPromptDto()
       {
          return GetList()[0]; 
       }

       public static PromptDto BuildPromptDto(string id, string nombre, string promtuser, string promtsystem, string tags, string folder)
       {
          return new MockDataBuilder<PromptDto>()
                    .With(x => x.Id,id)
                     .With(x => x.Nombre,nombre)
                     .With(x => x.Promtuser,promtuser)
                     .With(x => x.Promtsystem,promtsystem)
                     .With(x => x.Tags,tags)
                     .With(x => x.Folder,folder)
                     .Build();
       }

   }
}
