using System;
using System.Collections.Generic;
using Domain.AggregateModels.ValueObjects;

namespace UnitTestsProject.Tests.Test.MockData.Entities
{
    public static class MonedaMockData
    {

        public static List<Domain.AggregateModels.Moneda.Moneda> GetList()
        {

            return new List<Domain.AggregateModels.Moneda.Moneda>()
            {
                new("1", "1","1",RangoFechas.Create(DateTime.Now, DateTime.Now.AddDays(1)).Value){Codigo = 1,Id =BaseBuilder.GetGuIdValue("1", 0) },
                new("2", "2","2",RangoFechas.Create(DateTime.Now, DateTime.Now.AddDays(1)).Value){Codigo = 2,Id =BaseBuilder.GetGuIdValue("2", 0) },
                new("3", "3","3",RangoFechas.Create(DateTime.Now, DateTime.Now.AddDays(1)).Value){Codigo = 3,Id =BaseBuilder.GetGuIdValue("3",0 )},
            };
        }



    }
}
