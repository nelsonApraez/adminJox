using System;
using System.Collections.Generic;
using Application.Features.Models.Dto;

namespace UnitTestsProject.Tests.Test.MockData.Dtos
{
    public class MonedaDtoMockData
    {
        public static List<MonedaDto> GetList()
        {
            return new List<MonedaDto>()
            {
                BuildMonedaDto(BaseBuilder.GetGuIdValue("1",0).ToString(),1, "1", "1"),
                BuildMonedaDto(BaseBuilder.GetGuIdValue("2",0).ToString(),2, "2", "2"),
                BuildMonedaDto(BaseBuilder.GetGuIdValue("3",0).ToString(),3, "3", "3"),
                BuildMonedaDto(BaseBuilder.GetGuIdValue("4",0).ToString(),4, "4", "4")
            };
        }
        public static MonedaDto BuildMonedaDto()
        {
            return GetList()[0];
        }

        public static MonedaDto BuildMonedaDto(string id, int codigo = 1, string nombre = "1", string identificador = "1")
        {
            return new MockDataBuilder<MonedaDto>()
                .With(x => x.Id, id)
                .With(x => x.Codigo, codigo)
                .With(x => x.Nombre, nombre)
                .With(x => x.Identificador, identificador)
                .With(x => x.Descripcion, nameof(MonedaDto.Descripcion))
                .With(x => x.ActivoDesde, DateTime.Now)
                .With(x => x.ActivoHasta, DateTime.Now.AddDays(1))
                .Build();
        }
    }
}
