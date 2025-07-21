namespace UnitTestsProject.Tests.Test.MockData.Dtos
{
    using System;
    using System.Collections.Generic;
    using Application.Features.Models.Dto;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Respuesta]
    /// </summary>
    public static class RespuestaDtoMockData
    {
        public static List<RespuestaDto> GetList()
        {
            return new List<RespuestaDto>()
            {
                new RespuestaDto(){ Id =  BaseBuilder.GetGuIdValue("1",0).ToString(), 
                    Preguntaid = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Respuesta.Preguntaid), true), 
                    Valor = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Respuesta.Valor)) },
                new RespuestaDto(){ Id =  BaseBuilder.GetGuIdValue("2",0).ToString(), 
                    Preguntaid = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Respuesta.Preguntaid), true), 
                    Valor = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Respuesta.Valor)) },
                new RespuestaDto(){ Id =  BaseBuilder.GetGuIdValue("3",0).ToString(), 
                    Preguntaid = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Respuesta.Preguntaid), true), 
                    Valor = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Respuesta.Valor)) },
            };

        }

       public static RespuestaDto BuildRespuestaDto()
       {
          return GetList()[0]; 
       }

       public static RespuestaDto BuildRespuestaDto(string id, string preguntaid, string valor)
       {
          return new MockDataBuilder<RespuestaDto>()
                    .With(x => x.Id,id)
                     .With(x => x.Preguntaid,preguntaid)
                     .With(x => x.Valor,valor)
                     .Build();
       }

   }
}
