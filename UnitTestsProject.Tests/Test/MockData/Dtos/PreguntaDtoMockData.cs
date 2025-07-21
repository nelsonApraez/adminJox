namespace UnitTestsProject.Tests.Test.MockData.Dtos
{
    using System;
    using System.Collections.Generic;
    using Application.Features.Models.Dto;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Pregunta]
    /// </summary>
    public static class PreguntaDtoMockData
    {
        public static List<PreguntaDto> GetList()
        {
            return new List<PreguntaDto>()
            {
                new PreguntaDto(){ Id =  BaseBuilder.GetGuIdValue("1",0).ToString(), 
                    
                    Valor = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Pregunta.Valor)), 
                    Descripcion = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Pregunta.Descripcion)) },
                new PreguntaDto(){ Id =  BaseBuilder.GetGuIdValue("2",0).ToString(), 
                    Valor = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Pregunta.Valor)), 
                    Descripcion = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Pregunta.Descripcion)) },
                new PreguntaDto(){ Id =  BaseBuilder.GetGuIdValue("3",0).ToString(), 
                    Valor = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Pregunta.Valor)), 
                    Descripcion = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Pregunta.Descripcion)) },
            };

        }

       public static PreguntaDto BuildPreguntaDto()
       {
          return GetList()[0]; 
       }

       public static PreguntaDto BuildPreguntaDto(string id, string proyectoid, string valor, string descripcion)
       {
          return new MockDataBuilder<PreguntaDto>()
                    .With(x => x.Id,id)
                     .With(x => x.Valor,valor)
                     .With(x => x.Descripcion,descripcion)
                     .Build();
       }

   }
}
