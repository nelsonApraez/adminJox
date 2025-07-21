namespace UnitTestsProject.Tests.Test.MockData.Entities
{
    using Domain.AggregateModels.ValueObjects;
    using System;
    using System.Collections.Generic;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Pregunta]
    /// </summary>
    public static class PreguntaMockData
    {
        public static List<Domain.AggregateModels.Pregunta> GetList()
        {
            return new List<Domain.AggregateModels.Pregunta>()
            {
                new Domain.AggregateModels.Pregunta(
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.Valor)),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.Descripcion)),
                    BaseBuilder.GetValue<int>("1", nameof(Domain.AggregateModels.Pregunta.NumeroCategoria)),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.NombreCategoria))){ Id =  BaseBuilder.GetGuIdValue("1",0) },
                new Domain.AggregateModels.Pregunta(
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.Valor)),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.Descripcion)),
                    BaseBuilder.GetValue<int>("1", nameof(Domain.AggregateModels.Pregunta.NumeroCategoria)),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.NombreCategoria))){ Id =  BaseBuilder.GetGuIdValue("2",0) },
                new Domain.AggregateModels.Pregunta(
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.Valor)),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.Descripcion)),
                    BaseBuilder.GetValue<int>("1", nameof(Domain.AggregateModels.Pregunta.NumeroCategoria)),
                    BaseBuilder.GetValue<string>("1", nameof(Domain.AggregateModels.Pregunta.NombreCategoria))){ Id =  BaseBuilder.GetGuIdValue("3",0) }
            };

        }

    }
}
