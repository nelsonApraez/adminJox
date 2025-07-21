namespace Package.Utilities.Net
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Globalization;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Text;
    using System.Text.Json;

    /// <summary>
    /// Clase con los metodos extendidos para centralizar funcionalidades puntuales de cada tipo de objeto
    /// </summary>
    public static partial class ExtensionMethods
    {
        /// <summary>
        /// Se encarga de comparar dos objetos.
        /// </summary>
        /// <typeparam name="T">Tipo generico para un objeto.</typeparam>
        /// <param name="objectFromCompare">Objeto el cual se va a comparar.</param>
        /// <param name="objectToCompare">Objeto al cual se le compara.</param>
        /// <returns>Mensaje con el resultado de la comparación.</returns>
        public static string CompareEquals<T>(this T objectFromCompare, T objectToCompare)
            where T : class
        {
            string messageCompareEquals = ValidateCompareEquals(objectFromCompare, objectToCompare);

            if (string.IsNullOrEmpty(messageCompareEquals))
            {
                messageCompareEquals = GeFormatMessageAudit(objectFromCompare, objectToCompare);
            }

            return messageCompareEquals;
        }

        /// <summary>
        /// Se encarga de establecer el mensaje del formato de la auditoría de la diferencia de los objetos.
        /// </summary>
        /// <typeparam name="T">Tipo generico para un objeto.</typeparam>
        /// <param name="objectFromCompare">Objeto que se quiere comparar.</param>
        /// <param name="objectToCompare">Objeto al cual se va a comparar.</param>
        /// <returns>Mensaje del formato de la auditoría.</returns>
        private static string GeFormatMessageAudit<T>(T objectFromCompare, T objectToCompare) where T : class
        {
            const string formatMessageAudit = "<b>{0}</b>, Valor actual: {1}, Valor anterior: {2}";

            StringBuilder stringComparation = new StringBuilder();
            PropertyInfo[] properties = GetProperties<T>();
            string dataFromCompare;
            string dataToCompare;

            foreach (PropertyInfo property in properties)
            {
                dataFromCompare = Convert.ToString(property.GetValue(objectFromCompare, null) ?? "", CultureInfo.InvariantCulture);
                dataToCompare = Convert.ToString(property.GetValue(objectToCompare, null) ?? "", CultureInfo.InvariantCulture);

                if (dataFromCompare != dataToCompare)
                {
                    if (stringComparation.Length > 0)
                    {
                        stringComparation.Append("<br>");
                    }

                    stringComparation.Append(string.Format(CultureInfo.InvariantCulture, formatMessageAudit, property.Name, dataFromCompare, dataToCompare));
                }
            }

            return stringComparation.ToString();
        }

        /// <summary>
        /// Se encarga de obtener las propiedades de una clase.
        /// </summary>
        /// <typeparam name="T">Tipo generico para un objeto.</typeparam>
        /// <returns>Propiedades.</returns>
        private static PropertyInfo[] GetProperties<T>() where T : class
        {
            return typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.GetProperty);
        }

        /// <summary>
        /// Se encarga de validar los objetos a comparar.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objectFromCompare">Objeto que se quiere comparar.</param>
        /// <param name="objectToCompare">Objeto al cual se va a comparar.</param>
        /// <returns>Mensaje de validación.</returns>
        public static string ValidateCompareEquals<T>(T objectFromCompare, T objectToCompare) where T : class
        {
            string messageValidateCompareEquals = string.Empty;

            if (objectFromCompare == null && objectToCompare == null)
            {
                messageValidateCompareEquals = TextResponseApplication.Texts.Transversal.RegistroNuevoAntiguoVacio;
            }
            else if (objectFromCompare == null)
            {
                messageValidateCompareEquals = TextResponseApplication.Texts.Transversal.RegistroNuevoVacio;
            }
            else if (objectToCompare == null)
            {
                messageValidateCompareEquals = TextResponseApplication.Texts.Transversal.RegistroAntiguoVacio;
            }

            return messageValidateCompareEquals;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entity"></param>
        /// <param name="ignoredFields"></param>
        public static void TrimAll<T>(this T entity, params string[] ignoredFields)
        {
            Dictionary<Type, PropertyInfo[]> trimProperties = new Dictionary<Type, PropertyInfo[]>();
            if (!trimProperties.TryGetValue(typeof(T), out PropertyInfo[] props))
            {
                props = typeof(T)
                          .GetProperties()
                          .Where(c => !ignoredFields.Contains(c.Name) &&
                                      c.CanWrite &&
                                      c.PropertyType == typeof(string))
                          .ToArray();

                trimProperties.Add(typeof(T), props);
            }

            foreach (PropertyInfo property in props)
            {
                string value = Convert.ToString(property.GetValue(entity, null), CultureInfo.InvariantCulture);
                if (!string.IsNullOrEmpty(value))
                {
                    property.SetValue(entity, value.Trim(), null);
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valid"></param>
        /// <returns></returns>
        public static bool IsNull<T>([ValidatedNotNull] this T valid) where T : class => valid == null;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="messageBusinessException"></param>
        public static void IsValidThrow<T>([ValidatedNotNull] this List<T> lstValid, EnumerationMessage.Message messageBusinessException)
        {
            lstValid.IsValidThrow(messageBusinessException, EnumerationException.TypeCustomException.Validation);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s"></param>
        /// <param name="messageBusinessException"></param>
        public static void IsValidThrow<T>([ValidatedNotNull] this List<T> lstValid, EnumerationMessage.Message messageBusinessException,
                                                                                              EnumerationException.TypeCustomException typeCustomException)
        {
            if (!lstValid.IsNotNull())
            {
                throw new CustomException(typeCustomException, messageBusinessException);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderByMember"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query,
                                                      string orderByMember,
                                                      EnumerationApplication.Orden direction)
        {
            var OrderBy = ExpressionHelper.OrderBy(query, orderByMember, direction);
            if (OrderBy.IsNotNull())
            {
                return query.Provider.CreateQuery<T>(OrderBy);
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="orderByMember"></param>
        /// <param name="direction"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query,
                                                      Filter orderByMember)
        {
            if (orderByMember.Sorts.IsNotNull())
            {
                MethodCallExpression OrderBy;
                for (int i = 0; i < orderByMember.Sorts.Count; i++)
                {
                    if (!Enum.TryParse(orderByMember.Sorts[i].Direction.FirstCharToUpper(), out EnumerationApplication.Orden eOrden))
                    {
                        eOrden = EnumerationApplication.Orden.Asc;
                    }

                    OrderBy = (i == 0) ?
                                ExpressionHelper.OrderBy(query, orderByMember.Sorts[i].Name, eOrden) :
                                ExpressionHelper.ThenBy(query, orderByMember.Sorts[i].Name, eOrden);

                    if (OrderBy.IsNotNull())
                    {
                        query = query.Provider.CreateQuery<T>(OrderBy);
                    }
                }
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="whereMember"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereDynamic<T>(this IQueryable<T> query,
                                                    Filter whereMember)
        {
            if (whereMember.Filters.IsNotNull())
            {
                return CreateWhereDynamic(query, whereMember);
            }

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="whereMember"></param>
        /// <returns></returns>
        private static IQueryable<T> CreateWhereDynamic<T>(this IQueryable<T> query,
                                                           Filter whereMember)
        {
            Expression<Func<T, bool>> queryFilter = null;
            foreach (var itemWhere in whereMember.Filters)
            {
                if (itemWhere.Name.IsValid() &&
                    itemWhere.Values.IsNotNull() &&
                    itemWhere.Values.First().ToString().IsValid())
                {
                    queryFilter = AddConditionalQuery(queryFilter, itemWhere);

                    for (int i = 1; i < itemWhere.Values.Length; i++)
                    {
                        if (itemWhere.Values[i].ToString().IsValid())
                        {
                            queryFilter = queryFilter.Or(ExpressionHelper.GetCriteriaWhere<T>(itemWhere.Name,
                                                                                              itemWhere.Operator,
                                                                                              itemWhere.Values[i].ToString().Trim()));
                        }
                    }
                }
            }
            query = query.Where(queryFilter);

            return query;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="queryFilter"></param>
        /// <param name="itemWhere"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> AddConditionalQuery<T>(Expression<Func<T, bool>> queryFilter,
                                                           ItemsFilters itemWhere)
        {
            if (queryFilter.IsNotNull())
            {
                if (itemWhere.Conditional == EnumerationApplication.ConditionalExpression.Or)
                {
                    queryFilter = queryFilter.Or(ExpressionHelper.GetCriteriaWhere<T>(itemWhere.Name,
                                                                       itemWhere.Operator,
                                                                       itemWhere.Values.First().ToString().Trim()));

                }
                else
                {
                    queryFilter = queryFilter.And(ExpressionHelper.GetCriteriaWhere<T>(itemWhere.Name,
                                                                       itemWhere.Operator,
                                                                       itemWhere.Values.First().ToString().Trim()));
                }
            }
            else
            {
                queryFilter = ExpressionHelper.GetCriteriaWhere<T>(itemWhere.Name,
                                                                      itemWhere.Operator,
                                                                      itemWhere.Values.First().ToString().Trim());
            }

            return queryFilter;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstValid"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>([ValidatedNotNull] this Collection<T> lstValid) => (lstValid != null && lstValid.Count > 0);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstValid"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>([ValidatedNotNull] this ICollection<T> lstValid) => (lstValid != null && lstValid.Count > 0);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valid"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>([ValidatedNotNull] this T[] valid) => (valid != null && valid.Length > 0);

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valid"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>([ValidatedNotNull] this T valid) where T : class => valid != null;

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="valid"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>([ValidatedNotNull] this IEnumerable<T> valid) where T : class => (valid != null && valid.Any());

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="lstValid"></param>
        /// <returns></returns>
        public static bool IsNotNull<T>([ValidatedNotNull] this List<T> lstValid) => (lstValid != null && lstValid.Any());

        /// <summary>
        /// Serializar Objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objJsonConvert"></param>
        /// <returns></returns>
        public static string ToJsonSerialize<T>(this T objJsonConvert) => JsonSerializer.Serialize(objJsonConvert, new JsonSerializerOptions
        {
            IgnoreNullValues = true,
            PropertyNameCaseInsensitive = true
        });

        /// <summary>
        /// Serializar Objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objJsonConvert"></param>
        /// <returns></returns>
        public static string ToJsonSerialize<T>(this T objJsonConvert, JsonSerializerOptions jsonOptions) => JsonSerializer.Serialize(objJsonConvert, jsonOptions);

        /// <summary>
        /// Deserializar Objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objJsonConvert"></param>
        /// <returns></returns>
        public static T ToJsonDeserialize<T>(this string objJsonConvert)
             where T : class, new() => JsonSerializer.Deserialize<T>(objJsonConvert);

        /// <summary>
        /// Serializar Objeto
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="objJsonConvert"></param>
        /// <returns></returns>
        public static T ToJsonDeserialize<T>(this string objJsonConvert, JsonSerializerOptions jsonOptions) => JsonSerializer.Deserialize<T>(objJsonConvert, jsonOptions);
    }
}
