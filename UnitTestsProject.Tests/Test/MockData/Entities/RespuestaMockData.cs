namespace UnitTestsProject.Tests.Test.MockData.Entities
{
    using Domain.AggregateModels.ValueObjects;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Respuesta]
    /// </summary>
    public static class RespuestaMockData
    {
        public static List<Domain.AggregateModels.Respuesta> GetList()
        {
            return  new List<Domain.AggregateModels.Respuesta>()
            {
                new Domain.AggregateModels.Respuesta(
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Proyectoid), true),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Preguntaid), true), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Valor)) ){ Id =  BaseBuilder.GetGuIdValue("1",0) },
                new Domain.AggregateModels.Respuesta(
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Proyectoid), true),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Preguntaid), true), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Valor)) ){ Id =  BaseBuilder.GetGuIdValue("2",0) },
                new Domain.AggregateModels.Respuesta(
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Proyectoid), true),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Preguntaid), true), 
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Respuesta.Valor)) ){ Id =  BaseBuilder.GetGuIdValue("3",0) }
            };

        }

    }
}
