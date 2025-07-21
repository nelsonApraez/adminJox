namespace UnitTestsProject.Tests.Test.MockData.Dtos
{
    using System;
    using System.Collections.Generic;
    using Application.Features.Models.Dto;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Proyecto]
    /// </summary>
    public static class ProyectoDtoMockData
    {
        public static List<ProyectoDto> GetList()
        {
            return new List<ProyectoDto>()
            {
                new ProyectoDto(){ Id =  BaseBuilder.GetGuIdValue("1",0).ToString(), 
                    Nombre = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Nombre)), 
                    Tecnologias = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Tecnologias)), 
                    Descripcion = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Descripcion)) },
                new ProyectoDto(){ Id =  BaseBuilder.GetGuIdValue("2",0).ToString(), 
                    Nombre = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Nombre)), 
                    Tecnologias = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Tecnologias)), 
                    Descripcion = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Descripcion)) },
                new ProyectoDto(){ Id =  BaseBuilder.GetGuIdValue("3",0).ToString(), 
                    Nombre = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Nombre)), 
                    Tecnologias = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Tecnologias)), 
                    Descripcion = BaseBuilder.GetValue<string>("2", nameof(Domain.AggregateModels.Proyecto.Descripcion)) },
            };

        }

       public static ProyectoDto BuildProyectoDto()
       {
          return GetList()[0]; 
       }

       public static ProyectoDto BuildProyectoDto(string id, string nombre, string tecnologias, string descripcion)
       {
          return new MockDataBuilder<ProyectoDto>()
                    .With(x => x.Id,id)
                     .With(x => x.Nombre,nombre)
                     .With(x => x.Tecnologias,tecnologias)
                     .With(x => x.Descripcion,descripcion)
                     .Build();
       }

   }
}
