using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Infrastructure.Common;
using Microsoft.EntityFrameworkCore;
using Moq;
using UnitTestsProject.Tests.Components;

namespace UnitTestsProject.Tests
{
    public static class BaseBuilder
    {
        public static BaseMockData AddModelMockData<T>(this BaseMockData adaptater, List<T> sourceList) where T : class, new()
        {
            DbSet<T> myDbSet = QueryRepositoryTestExtensions.GetQueryableMockDbSet(sourceList);
            adaptater.AdaptaterMock.Setup(item => item.Set<T>()).Returns(myDbSet);
            return adaptater;
        }

        public static BaseMockData AddModelDbContextMockData<T>(this BaseMockData adaptater, List<T> sourceList) where T : class, new()
        {
            sourceList.ForEach(item =>
            {
                try
                {
                    adaptater.AdaptaterDbContext.Set<T>().Add(item);
                    adaptater.AdaptaterDbContext.SaveChanges();
                }
                catch (Exception)
                {
                    adaptater.AdaptaterDbContext.ChangeTracker.Clear();
                }
            });
            return adaptater;
        }

        /// <summary>
        /// Metodo Transversal para cargar valores a los objetos de la entidad
        /// </summary>
        /// <typeparam name="TValue">Tipo de dato</typeparam>
        /// <param name="value">Valor</param>
        /// <param name="name">Nombre de la propiedad de la entidad</param>
        /// <returns></returns>
        /// 
        public static TValue SetValue<TValue>(string value, string name)
        {
            return ((TValue)Convert.ChangeType(
                ((typeof(TValue) == typeof(string)) ? string.Concat(name, " ", value) : value),
                typeof(TValue)));
        }

        /// <summary>
        /// Metodo Transversal para cargar valores a los objetos de la entidad
        /// </summary>
        /// <typeparam name="TValue">Tipo de dato</typeparam>
        /// <param name="value">Valor</param>
        /// <param name="name">Nombre de la propiedad de la entidad</param>
        /// <returns></returns>
        public static TValue GetValue<TValue>(string value, string name, bool isGuId = false)
        {
            if (isGuId)
            {
                return (TValue)Convert.ChangeType(GetGuIdValue(value).ToString(), typeof(TValue));
            }
            else
            {
                name = AddBehaviorObjects(name);
                if (typeof(TValue).Name.ToLower().Contains("bool"))
                    return (TValue)Convert.ChangeType(true, typeof(bool));
                if (typeof(TValue).Name.ToLower().Contains("datetime"))
                    return (TValue)Convert.ChangeType(DateTime.Now, typeof(DateTime));
                return ((TValue)Convert.ChangeType(
                    ((typeof(TValue) == typeof(string)) ? string.Concat(name, " ", value) : value),
                    typeof(TValue)));
            }
        }

        /// <summary>
        /// Metodo Transversal para cargar valores a los objetos de la entidad por GUID
        /// </summary>
        /// <typeparam name="TValue">Tipo de dato</typeparam>
        /// <param name="value">Valor</param>        
        /// <returns></returns>
        public static Guid GetGuIdValue(string value, int valor2 = 0)
        {
            string valor = value.PadLeft(32, valor2.ToString().ToCharArray()[0]);
            return Guid.TryParse(valor, out Guid guidvalor) ? guidvalor : Guid.Empty;
        }

     

        /// <summary>
        /// Metodo virtualizado para realizar customizaciones de valores de objetos transversales de la entidad
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public static string AddBehaviorObjects(string name) { return name; }
    }

    public class BaseMockData
    {
        public Mock<Domain.Common.IMainContext> AdaptaterMock;
        public MainContext AdaptaterDbContext;
        public BaseMockData()
        {
            AdaptaterMock = new Mock<Domain.Common.IMainContext>();

            AdaptaterMock.Setup(x => x.SaveChangesAsync(default)).Returns(Task.FromResult(1));
            AdaptaterDbContext = InMemoryDbContextFactory.GetDbContext(null);
        }

        public Domain.Common.IMainContext Object => AdaptaterMock.Object;
    }
}
