namespace Infrastructure.Common
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Dynamic.Core;
    using System.Linq.Expressions;
    using Domain.Common.Enums;
    using Domain.Common.ValueObjects;
    using Package.Utilities.Net;

    /// <summary>
    /// Clase Base que permitira centralizar las configuraciones de ParameterOfList para la construcción de Querys Dinamicos para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/>
    /// </summary>
    /// <typeparam name="T">Entidad de Negocio</typeparam>
    public abstract partial class BaseRepositoryDao<T>
          where T : class, new()
    {
        /// <summary>
        /// Configuarar Objeto, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <returns>Querys Dinamicos <paramref name="iQuery"/> para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/></returns>
        protected virtual IQueryable<T> ConfigureParameterOfList(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList)
        {

            if (parameterOfList.IsNotNull())
            {
                iQuery = ConfigureOrderBy(iQuery, parameterOfList);
                iQuery = ConfigureParameterBaseOfList(iQuery, parameterOfList);
            }

            return iQuery;
        }

        /// <summary>
        /// Configuarar Objeto, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <returns>Querys Dinamicos <paramref name="iQuery"/> para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/></returns>
        protected virtual IQueryable<T> ConfigureParameterBaseOfList(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList)
        {

            if (parameterOfList.IsNotNull())
            {
                iQuery = ConfigureParameterValueObjectsOfList(iQuery, parameterOfList);
                iQuery = ConfigureFilter(iQuery, parameterOfList);
                iQuery = ConfigureInclude(iQuery, parameterOfList);
                iQuery = ConfigureMaxCount(iQuery, parameterOfList);
                iQuery = ConfigurePaged(iQuery, parameterOfList);
            }

            return iQuery;
        }

        /// <summary>
        /// Configuarar Objeto, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <returns>Querys Dinamicos <paramref name="iQuery"/> para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/></returns>
        protected IQueryable<T> ConfigureParameterValueObjectsOfList(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList)
        {

            if (parameterOfList.IsNotNull())
            {

                //se identifica si en el filtro esta el campo ValueObjects 
                List<string> list = new();
                Action<Type, string, List<string>> listDel = (x, y, l) => l.AddRange((from asm in x.GetProperties()
                                                                                      where asm.MemberType == System.Reflection.MemberTypes.Property && asm.PropertyType.BaseType != null
                                                                                      && (asm.PropertyType.BaseType.Name.Contains(nameof(ValueObjectString)))
                                                                                      select y.ToLower() + asm.Name.ToLower())?.ToList());
                //Se unen con las propiedaes de navegacion de la clase
                listDel(typeof(T), "", list);
                typeof(T).GetProperties()
                     .Where(a => a.PropertyType.BaseType != null).ToList()
                     .ForEach(a =>
                     {
                         a.PropertyType.GetProperties()
                                       .Where(y => y.PropertyType.BaseType != null).ToList()
                                       .ForEach(y => listDel(y.PropertyType, a.Name + "." + y.Name + ".", list));
                         listDel(a.PropertyType, a.Name + ".", list);
                     });
                if (list.Count > 0)
                {
                    //Filtros por campos ValueObject
                    if (parameterOfList.WhereDynamic.Filters.IsNotNull() && parameterOfList.WhereDynamic.Filters.Any(x => list.Any(a => a == x.Name.ToLower())))
                        parameterOfList.WhereDynamic.Filters?.Where(x => list.Any(a => a == x.Name.ToLower())).ToList().ForEach(item => item.Name += "." + nameof(ValueObjectString.Valor));
                    //Ordenamiento Campos ValueObject
                    if (parameterOfList.WhereDynamic.Sorts.IsNotNull() && parameterOfList.WhereDynamic.Sorts.Any(x => list.Any(a => a == x.Name.ToLower())))
                    {
                        var listShort = parameterOfList.WhereDynamic.Sorts.Where(x => list.Any(a => a == x.Name.ToLower())).ToList();
                        for (int i = 0; i < listShort.Count; i++)
                        {
                            listShort[i] = new() { Name = listShort[i].Name + "." + nameof(ValueObjectString.Valor), Direction = listShort[i].Direction };
                        }
                        parameterOfList.WhereDynamic.Sorts.RemoveAll(x => list.Any(a => a == x.Name.ToLower()));
                        parameterOfList.WhereDynamic.Sorts.AddRange(listShort);
                    }
                    if (parameterOfList.OrderByDynamic.IsNotNull() && list.Any(a => a == parameterOfList.OrderByDynamic.Item1.ToLower()))
                    {
                        parameterOfList.OrderByDynamic = new System.Tuple<string, EnumerationApplication.Orden>(parameterOfList.OrderByDynamic.Item1 + "." + nameof(ValueObjectString.Valor), parameterOfList.OrderByDynamic.Item2);
                    }
                }
            }
            return iQuery;
        }

        /// <summary>
        /// Configuarar Objeto, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta para campos calculados Estado por ordenamiento y filtro
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <param name="expressionEnable">Expresion lambda de consulta para campo calculado activo</param>
        /// <param name="expressionDisable">Expresion lambda de consulta para campo calculado Inactivo</param>
        /// <param name="nameState">Nombre de parametro estado</param>
        /// <returns></returns>
        protected virtual IQueryable<T> ConfigureEstateFilterParameterOfList(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList, Expression<Func<T, bool>> expressionEnable, Expression<Func<T, bool>> expressionDisable, string nameState)
        {
            if (parameterOfList.IsNotNull())
            {
                //se identifica si en el filtro esta el campo Estado 
                if (parameterOfList.WhereDynamic.Filters.IsNotNull() && parameterOfList.WhereDynamic.Filters.Any(x => x.Name == nameState))
                {
                    //se eliminan los filtros por el campo Estado
                    iQuery = parameterOfList.WhereDynamic.Filters
                        .Any(x => x.Name == nameState &&
                         x.Values
                                .Any(x => x.ToString() == EnumsEstado.ACTIVO || x.ToString().ToLower() == true.ToString().ToLower()))
                        ? iQuery.Where(expressionEnable)
                        : iQuery.Where(expressionDisable);
                    parameterOfList.WhereDynamic.Filters.RemoveAll(x => x.Name == nameState);
                }

                //se identifica si hay ordenamiento por Estado
                if (parameterOfList.WhereDynamic.Sorts.IsNotNull() && parameterOfList.WhereDynamic.Sorts.Any(x => x.Name == nameState))
                {
                    iQuery = ConfigureParameterValueObjectsOfList(iQuery, parameterOfList);
                    return ConfigureEstateOrderbyParameterOfList(iQuery, parameterOfList, expressionEnable, nameState);
                }
                else if (parameterOfList.OrderByDynamic.IsNotNull() && parameterOfList.OrderByDynamic.Item1 == nameState)
                {
                    iQuery = parameterOfList.OrderByDynamic.Item1 == nameState
                    && parameterOfList.OrderByDynamic.Item2.ToString().ToLower() == EnumerationApplication.Orden.Desc.ToString().ToLower()
                        ? iQuery.OrderBy(expressionEnable)
                        : iQuery.OrderByDescending(expressionEnable);
                    return ConfigureParameterBaseOfList(iQuery, parameterOfList);
                }
                iQuery = ConfigureOrderBy(iQuery, parameterOfList);
            }
            iQuery = ConfigureParameterBaseOfList(iQuery, parameterOfList);
            return iQuery;
        }

        /// <summary>
        /// Configuarar Objeto, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta para campos calculados Estado por ordenamiento y filtro
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <param name="expressionEnable">Expresion lambda de consulta para campo calculado activo</param>        
        /// <param name="nameState">Nombre de parametro estado</param>
        /// <returns></returns>
        protected virtual IQueryable<T> ConfigureEstateOrderbyParameterOfList(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList, Expression<Func<T, bool>> expressionEnable, string nameState)
        {
            MethodCallExpression orderByPrimary;
            for (int i = 0; i < parameterOfList.WhereDynamic.Sorts.Count; i++)
            {
                if (!Enum.TryParse(parameterOfList.WhereDynamic.Sorts[i].Direction.FirstCharToUpper(), out EnumerationApplication.Orden eOrden))
                {
                    eOrden = EnumerationApplication.Orden.Asc;
                }
                if (parameterOfList.WhereDynamic.Sorts[i].Name == nameState)
                {
                    if (i == 0)
                    {
                        iQuery = eOrden == EnumerationApplication.Orden.Desc
                        ? iQuery.OrderBy(expressionEnable)
                        : iQuery.OrderByDescending(expressionEnable);
                    }
                    else
                    {
                        var keySelector = expressionEnable;
                        var orderBy = Expression.Call(
                                                    typeof(Queryable),
                                                    eOrden == EnumerationApplication.Orden.Asc ? "ThenBy" : "ThenByDescending",
                                                     new[] { typeof(T), keySelector.Body.Type },
                                                    iQuery.Expression,
                                                    Expression.Quote(keySelector));


                        if (orderBy.IsNotNull())
                        {
                            iQuery = iQuery.Provider.CreateQuery<T>(orderBy);
                        }
                    }
                }
                else
                {
                    orderByPrimary = (i == 0) ?
                                ExpressionHelper.OrderBy(iQuery, parameterOfList.WhereDynamic.Sorts[i].Name, eOrden) :
                                ExpressionHelper.ThenBy(iQuery, parameterOfList.WhereDynamic.Sorts[i].Name, eOrden);
                    if (orderByPrimary.IsNotNull())
                    {
                        iQuery = iQuery.Provider.CreateQuery<T>(orderByPrimary);
                    }
                }

            }
            //se eliminan el ordenamiento por el campo Estado
            parameterOfList.WhereDynamic.Sorts?.RemoveAll(x => x.Name == nameState);
            return ConfigureParameterBaseOfList(iQuery, parameterOfList);
        }






        /// <summary>
        /// Configuarar OrderBy, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <returns>Querys Dinamicos <paramref name="iQuery"/> para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/></returns>
        private IQueryable<T> ConfigureOrderBy(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList)
        {
            iQuery = ConfigureParameterValueObjectsOfList(iQuery, parameterOfList);
            if (parameterOfList.OrderBy.IsNotNull())
            {
                iQuery = parameterOfList.OrderBy(iQuery);
            }

            if (parameterOfList.WhereDynamic.Sorts.IsNotNull())
            {
                iQuery = iQuery.OrderByDynamic(parameterOfList.WhereDynamic);
            }
            else
            {
                if (parameterOfList.OrderByDynamic.IsNotNull() && parameterOfList.OrderByDynamic.Item1.IsValid())
                {
                    iQuery = iQuery.OrderByDynamic(parameterOfList.OrderByDynamic.Item1, parameterOfList.OrderByDynamic.Item2);
                }
            }

            return iQuery;
        }

        /// <summary>
        /// Configuarar Filter, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <returns>Querys Dinamicos <paramref name="iQuery"/> para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/></returns>
        protected virtual IQueryable<T> ConfigureFilter(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.Filter.IsNotNull())
            {
                iQuery = iQuery.Where(parameterOfList.Filter);
            }

            if (parameterOfList.WhereDynamic.Filters.IsNotNull() && !parameterOfList.WhereDynamic.Filters.Select(a => a.Values).ToList().Any(x => x.FirstOrDefault() == null))
            {
                iQuery = iQuery.WhereDynamic(parameterOfList.WhereDynamic);
            }

            return iQuery;
        }


        /// <summary>
        /// Configuarar MaxCount, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <returns>Querys Dinamicos <paramref name="iQuery"/> para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/></returns>
        public static IQueryable<T> ConfigureMaxCount(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.Skip > -1)
            {
                parameterOfList.MaxCount = iQuery.LongCount();
            }

            return iQuery;
        }

        /// <summary>
        /// Configuarar Paged, <paramref name="parameterOfList"/> para generar el IQueryable de Consulta
        /// </summary>
        /// <param name="iQuery">IQueryable de la Consulta</param>
        /// <param name="parameterOfList">Objeto de Configuraciones de Querys Dinamicos</param>
        /// <returns>Querys Dinamicos <paramref name="iQuery"/> para base de datos o persistencia de todas las entidades de negocio <typeparamref name="T"/></returns>
        public static IQueryable<T> ConfigurePaged(IQueryable<T> iQuery, ParameterOfList<T> parameterOfList)
        {
            if (parameterOfList.Take > 0)
            {
                if (parameterOfList.Skip > -1)
                {
                    iQuery = iQuery.Skip(parameterOfList.Skip);
                }

                iQuery = iQuery.Take(parameterOfList.Take);
            }
            else
            {
                if (parameterOfList.Skip > -1)
                {
                    parameterOfList.Take = Constants.DefaultNumeroDeRegistros;

                    iQuery = iQuery
                        .Skip(parameterOfList.Skip)
                        .Take(parameterOfList.Take);
                }
            }

            return iQuery;
        }
    }
}
