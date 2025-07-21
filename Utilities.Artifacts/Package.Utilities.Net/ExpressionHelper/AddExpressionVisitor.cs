namespace Package.Utilities.Net
{
    using System.Linq.Expressions;

    /// <summary>
    /// Clase Encalsuladora para adicion de condiciones para la construcción Expression Dinamicas para generar consultas genericas.
    /// </summary>
    public class AddExpressionVisitor : ExpressionVisitor
    {
        /// <summary>
        /// Propiedades para adicionar los Expression
        /// </summary>
        private readonly Expression from;

        /// <summary>
        /// Propiedades para adicionar los Expression
        /// </summary>
        private readonly Expression to;

        /// <summary>
        /// Metodo Contructor para adicionar expressions
        /// </summary>
        /// <param name="from">Expression Desde</param>
        /// <param name="to">Expression a</param>
        public AddExpressionVisitor(Expression from, Expression to)
        {
            this.from = from;
            this.to = to;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="node"></param>
        /// <returns></returns>
        public override Expression Visit(Expression node)
        {
            return node == from ? to : base.Visit(node);
        }
    }
}
