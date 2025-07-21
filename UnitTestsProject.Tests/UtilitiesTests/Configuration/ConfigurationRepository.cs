using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using Package.Utilities.Net;

namespace UnitTestsProject.Tests
{
    public static class RepositoryTestExtensions
    {


        /// <summary>
        /// retorna el nombre de la propiedad que es la llave de la entidad ENT
        /// </summary>
        /// <returns></returns>
        public static string GetKeyName<ENT>()
        {
            var keyDefaul = (from asm in typeof(ENT).GetProperties()
                             where asm.MemberType == System.Reflection.MemberTypes.Property
                             && asm.CustomAttributes?.FirstOrDefault()?.AttributeType?.FullName == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)?.ToString()
                             select asm.Name).FirstOrDefault();
            return !String.IsNullOrEmpty(keyDefaul) ? keyDefaul : (from asm in typeof(ENT).GetProperties()
                                                                   where asm.MemberType == System.Reflection.MemberTypes.Property
                                                                   select asm.Name).FirstOrDefault();
        }


        /// <summary>
        /// retorna el nombre de la propiedad que es la llave de la entidad ENT
        /// </summary>
        /// <returns></returns>
        public static PropertyInfo GetKeyType<ENT>()
        {
            var keyDefaul = (from asm in typeof(ENT).GetProperties()
                             where asm.MemberType == System.Reflection.MemberTypes.Property
                             && asm.CustomAttributes?.FirstOrDefault()?.AttributeType?.FullName == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)?.ToString()
                             select asm).FirstOrDefault();
            return keyDefaul ?? (from asm in typeof(ENT).GetProperties()
                                 where asm.MemberType == System.Reflection.MemberTypes.Property
                                 select asm).FirstOrDefault();
        }


        /// <summary>
        /// Crea Expresion lambda desde clase entidad de dominio
        /// </summary>
        /// <typeparam name="ENT"></typeparam>
        /// <param name="value"></param>
        /// <returns></returns>
        public static Expression<Func<ENT, bool>> GetExpressionEqual<ENT>(int value)
        {
            var typeKey = RepositoryTestExtensions.GetKeyType<ENT>().PropertyType.Name.ToLower();
            ParameterExpression parameterExpression = Expression.Parameter(typeof(ENT), typeof(ENT).Name.Substring(0, 1).ToLower());
            var constant = Expression.Constant(typeKey == "guid" ? BaseBuilder.GetGuIdValue(value.ToString()) : value);
            var property = Expression.Property(parameterExpression, RepositoryTestExtensions.GetKeyName<ENT>());
            var expression = Expression.Equal(property, constant);
            return Expression.Lambda<Func<ENT, bool>>(expression, parameterExpression);
        }

        /// <summary>
        /// Crea parametros de consulta genericos desde lalve de entidad ENT
        /// </summary>
        /// <returns></returns>
        public static ParameterOfList<ENT> GetFilterParameterOfListBase<ENT>() where ENT : class, new()
        {
            return new ParameterOfList<ENT>(1, 2, RepositoryTestExtensions.GetKeyName<ENT>(), "Asc", new Filter()
            {
                Filters = new List<ItemsFilters>()
                 {
                     new() {
                         Name = RepositoryTestExtensions.GetKeyName<ENT>(),
                         Values = new[] { "1" },
                         Operator = EnumerationApplication.OperationExpression.Equals
                     }
                 }
            });
        }

        /// <summary>
        /// Metodo virtualizado para implementar en las entidades mock de objetos necesario para las pruebas unitarias
        /// </summary>
        public static BaseMockData AddAdapterBaseMock(this BaseMockData adaptaterBase)
        {
            return adaptaterBase;
        }
    }
}
