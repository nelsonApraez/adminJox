namespace UnitTestsProject.Tests.Test.MockData.Entities
{
    using Domain.AggregateModels.ValueObjects;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Proyecto]
    /// </summary>
    public static class ProyectoMockData
    {
        public static List<Domain.AggregateModels.Proyecto> GetList()
        {
            return  new List<Domain.AggregateModels.Proyecto>()
            {
                new Domain.AggregateModels.Proyecto( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Nombre)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Tecnologias)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Descripcion)) ){ Id =  BaseBuilder.GetGuIdValue("1",0) },
                new Domain.AggregateModels.Proyecto( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Nombre)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Tecnologias)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Descripcion)) ){ Id =  BaseBuilder.GetGuIdValue("2",0) },
                new Domain.AggregateModels.Proyecto( 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Nombre)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Tecnologias)), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Proyecto.Descripcion)) ){ Id =  BaseBuilder.GetGuIdValue("3",0) }
            };

        }

    }
}
