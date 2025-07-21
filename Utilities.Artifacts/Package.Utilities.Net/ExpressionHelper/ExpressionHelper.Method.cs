namespace Package.Utilities.Net
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;

    /// <summary>
    /// Clase helper para la construcción Expression Dinamicas para generar consultas genericas.
    /// </summary>
    public static partial class ExpressionHelper
    {
        /// <summary>
        /// Excepción A Retornar cuando no este implementado un tipo "OperationExpression" para su Expression.
        /// </summary>
        private const string messageException = "Not implement Operation";

        /// <summary>
        /// 
        /// </summary>
        /// <param name="prop"></param>
        /// <param name="fieldValue"></param>
        /// <param name="memberExpression"></param>
        /// <param name="methodInfo"></param>
        /// <returns></returns>
        private static MethodCallExpression ExpressionMethodString(PropertyDescriptor prop, object fieldValue,
                                                                   MemberExpression memberExpression, MethodInfo methodInfo)
        {
            MethodCallExpression bodyLike;
            if (typeof(string) != prop.PropertyType)
            {
                var toString = typeof(Object).GetMethod("ToString");
                var toStringValue = Expression.Call(memberExpression, toString);

                bodyLike = Expression.Call(toStringValue, methodInfo, Expression.Constant(fieldValue, typeof(string)));
            }
            else
            {
                bodyLike = Expression.Call(memberExpression, methodInfo, Expression.Constant(fieldValue, prop.PropertyType));
            }

            return bodyLike;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="parameter"></param>
        /// <param name="propName"></param>
        /// <returns></returns>
        private static MemberExpression GetMemberExpression(ParameterExpression parameter, string propName)
        {
            if (string.IsNullOrEmpty(propName))
            {
                return null;
            }

            var propertiesName = propName.Split('.');
            if (propertiesName.Count() > 1)
            {
                MemberExpression properties = Expression.Property(parameter, propertiesName[0]);
                for (int i = 1; i < propertiesName.Count(); i++)
                {
                    properties = Expression.Property(properties, propertiesName[i]);
                }

                return properties;
            }

            return Expression.Property(parameter, propName);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="fieldValue"></param>
        /// <param name="parameterExpression"></param>
        /// <param name="memberExpression"></param>
        /// <returns></returns>
        private static Expression<Func<T, bool>> Contains<T>(object fieldValue, ParameterExpression parameterExpression, MemberExpression memberExpression)
        {
            var list = (List<long>)fieldValue;

            if (list == null || list.Count == 0)
            {
                return x => true;
            }

            MethodInfo containsInList = typeof(List<long>).GetMethod("Contains", new Type[] { typeof(long) });
            var bodyContains = Expression.Call(Expression.Constant(fieldValue), containsInList, memberExpression);
            return Expression.Lambda<Func<T, bool>>(bodyContains, parameterExpression);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="props"></param>
        /// <param name="fieldName"></param>
        /// <param name="ignoreCase"></param>
        /// <returns></returns>
        private static PropertyDescriptor GetProperty(PropertyDescriptorCollection props, string fieldName, bool ignoreCase)
        {
            if (!fieldName.Contains('.'))
            {
                return props.Find(fieldName, ignoreCase);
            }

            var fieldNameProperty = fieldName.Split('.');
            var properties = props.Find(fieldNameProperty[0], ignoreCase);
            for (int i = 1; i < fieldNameProperty.Count(); i++)
            {
                properties = properties.GetChildProperties().Find(fieldNameProperty[i], ignoreCase);
            }

            return properties;
        }
    }
}
