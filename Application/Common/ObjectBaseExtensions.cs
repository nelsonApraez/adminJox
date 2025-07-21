using System;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Domain.Common.Enums;
using OfficeOpenXml.FormulaParsing.Excel.Functions.Text;
using Package.Utilities.Net;

namespace Application.Common
{
    public static class ObjectBaseExtensions
    {
        /// <summary>
        /// Obtener la propiedad primary key de la entidad
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static string GetPrimaryKey<T>()
        {
            String primaryKey = (from asm in typeof(T).GetProperties()
                                 where asm.MemberType == System.Reflection.MemberTypes.Property
                                 && asm.CustomAttributes?.FirstOrDefault()?.AttributeType?.FullName == typeof(System.ComponentModel.DataAnnotations.KeyAttribute)?.ToString()
                                 select asm.Name).FirstOrDefault();

            return !String.IsNullOrEmpty(primaryKey) ? primaryKey : (from asm in typeof(T).GetProperties()
                                                                     where asm.MemberType == System.Reflection.MemberTypes.Property
                                                                     select asm.Name).FirstOrDefault();
        }


        /// <summary>
        /// Obtener el valor de la propiedad primary key <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T">Referencia del objeto</typeparam>
        /// <param name="value">Valor de la Primary Key</param>
        /// <returns></returns>

        public static object GetPrimaryKeyValue<T>(string value)
        {
            return typeof(T).GetProperty(GetPrimaryKey<T>()).PropertyType.Name switch
            {
                nameof(String) or nameof(Int32) or nameof(UInt64) or nameof(Decimal) => Convert.ToInt64(value),
                nameof(Guid) => Guid.TryParse(value, out Guid guidResult) ? guidResult : Guid.Empty,
                _ => value,
            };
        }

        /// <summary>
        /// Obtener el valor de la propiedad primary key de la entidad <typeparamref name="T"/>
        /// </summary>
        /// <param name="T">Referencia del objeto</param>
        /// <returns>Valor de la Primary Key</returns>
        public static object GetStringKeyValue<T>(T entity, string value = "")
        {
            return entity.GetType().GetProperty(string.IsNullOrEmpty(value) ? GetPrimaryKey<T>() : value).GetValue(entity, null);
        }

        public static Expression<Func<T, bool>> GetQueryFilterId<T>(T oEntity)
        {
            return ExpressionHelper.GetCriteriaWhere<T>(GetPrimaryKey<T>(),
                                                                    EnumerationApplication.OperationExpression.Equals,
                                                                    ObjectBaseExtensions.GetStringKeyValue<T>(oEntity));

        }



        /// <summary>
        /// Serializacion de fechas basado en formatos estandar
        /// </summary>
        /// <param name="fechaTexto"></param>
        /// <returns></returns>
        public static DateTime ConvertirTextoAFecha(string fechaTexto)
        {
            DateTime fechaConFormato = new();
            string[] formats = { EnumFormatosFecha.FORMATO1, EnumFormatosFecha.FORMATO2, EnumFormatosFecha.FORMATO3, EnumFormatosFecha.FORMATO4 };
            if (DateTime.TryParseExact(fechaTexto, formats, CultureInfo.InvariantCulture, DateTimeStyles.None, out DateTime fecha))
            {
                fechaConFormato = fecha;
            }
            return fechaConFormato;
        }

        /// <summary>
        ///  Obtener el valor de la propiedad de la entidad <typeparamref name="T"/>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="item">Referencia del objeto</param>
        /// <param name="propertyName">Nombre de propiedad del objeto del objeto <typeparamref name="T"/> </param>
        /// <returns></returns>
        public static string GetPropertyValue<T>(T item, string propertyName)
        {
            var value = item.GetType()?.GetProperty(propertyName)?.GetValue(item, null);

            if (item.GetType()?.GetProperty(propertyName)?.PropertyType?.BaseType?.Name?.Contains(ValueObjectEnum.VALUE_OBJECT) == true)
            {
                value = value?.GetType()?.GetProperty(ValueObjectEnum.VALOR)?.GetValue(value, null);
            }

            return value?.ToString();
        }

    }
}
