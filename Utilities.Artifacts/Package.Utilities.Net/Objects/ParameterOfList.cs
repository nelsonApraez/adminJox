namespace Package.Utilities.Net
{
    using System;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// Objeto para construccion de Querys Dinamicos con el IQueryable
    /// </summary>
    /// <typeparam name="T">Objeto</typeparam>
    public partial class ParameterOfList<T>
        where T : class
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public ParameterOfList()
        {
            Clear();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        public ParameterOfList(Expression<Func<T, bool>> expression) : this()
        {
            this.Filter = expression;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include"></param>
        public ParameterOfList(Expression<Func<T, bool>> expression,
                                    params Expression<Func<T, object>>[] include) : this(expression)
        {
            Include = include;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="include"></param>
        public ParameterOfList(int page, int pageSize, Expression<Func<T, bool>> expression,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBY,
                                    params Expression<Func<T, object>>[] include) : this(page, pageSize, expression, orderBY)
        {
            Include = include;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderBY"></param>
        public ParameterOfList(Expression<Func<T, bool>> expression,
                                     Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(expression) { this.OrderBy = orderBY; }

        /// <summary>
        ///
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="expression"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        public ParameterOfList(int page, int pageSize, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden, Filter whereDynamic)
            : this(page, expression, orderByDynamic, DirecOrden)
        {
            this.Take = pageSize;
            WhereDynamic = whereDynamic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="expression"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        public ParameterOfList(Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden) : this(expression)
        {
            ConfigurateOrderByDynamic(orderByDynamic, DirecOrden);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        /// <param name="include"></param>
        public ParameterOfList(Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden, params Expression<Func<T, object>>[] include)
            : this(expression, orderByDynamic, DirecOrden)
        {
            Include = include;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="expression"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        /// <param name="include"></param>
        public ParameterOfList(int page, int pageSize, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden, params Expression<Func<T, object>>[] include)
            : this(page, pageSize, expression, orderByDynamic, DirecOrden)
        {
            Include = include;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="expression"></param>
        public ParameterOfList(int page, Expression<Func<T, bool>> expression) : this(page, expression, null) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="expression"></param>
        /// <param name="orderBY"></param>
        public ParameterOfList(int page, Expression<Func<T, bool>> expression,
                                    Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(expression, orderBY) { this.Page = page; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="expression"></param>
        public ParameterOfList(int page, int pageSize,
                                Expression<Func<T, bool>> expression) : this(page, pageSize, expression, null)
        { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="expression"></param>
        /// <param name="orderBY"></param>
        public ParameterOfList(int page, int pageSize,
                                Expression<Func<T, bool>> expression,
                                Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(page, expression, orderBY)
        {
            this.Take = pageSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="expression"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBY"></param>
        public ParameterOfList(Expression<Func<T, bool>> expression, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(expression, orderBY) { this.Take = pageSize; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderBY"></param>
        public ParameterOfList(int page, int pageSize, Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(page, pageSize, null, orderBY) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="orderBY"></param>
        public ParameterOfList(int page, Func<IQueryable<T>, IOrderedQueryable<T>> orderBY) : this(page, null, orderBY) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="expression"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        public ParameterOfList(int page, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden) : this(page, expression, null)
        {
            ConfigurateOrderByDynamic(orderByDynamic, DirecOrden);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="expression"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        /// <param name="include"></param>
        public ParameterOfList(int page, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden,
                                    params Expression<Func<T, object>>[] include) : this(page, expression, orderByDynamic, DirecOrden)
        {
            Include = include;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        public ParameterOfList(int page, int pageSize, string orderByDynamic, string DirecOrden) : this(page, null, orderByDynamic, DirecOrden) { this.Take = pageSize; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="expression"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        public ParameterOfList(int page, int pageSize, Expression<Func<T, bool>> expression, string orderByDynamic, string DirecOrden)
            : this(page, expression, orderByDynamic, DirecOrden)
        { this.Take = pageSize; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        public ParameterOfList(int page, string orderByDynamic, string DirecOrden) : this(page, null, orderByDynamic, DirecOrden) { }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        /// <param name="include"></param>
        public ParameterOfList(int page, string orderByDynamic, string DirecOrden, params Expression<Func<T, object>>[] include)
            : this(page, orderByDynamic, DirecOrden)
        {
            Include = include;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        /// <param name="whereDynamic"></param>
        public ParameterOfList(int page, int pageSize, string orderByDynamic, string DirecOrden, Filter whereDynamic) :
            this(page, null, orderByDynamic, DirecOrden)
        {
            this.Take = pageSize;
            WhereDynamic = whereDynamic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="orderByDynamic"></param>
        /// <param name="DirecOrden"></param>
        public ParameterOfList(string orderByDynamic, string DirecOrden) : this()
        {
            ConfigurateOrderByDynamic(orderByDynamic, DirecOrden);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="whereDynamic"></param>
        public ParameterOfList(Filter whereDynamic) : this()
        {
            WhereDynamic = whereDynamic;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="pageSize"></param>
        public ParameterOfList(int pageSize) : this()
        {
            this.Take = pageSize;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="include"></param>
        public ParameterOfList(params Expression<Func<T, object>>[] include) : this()
        {
            this.Include = include;
        }
    }
}
