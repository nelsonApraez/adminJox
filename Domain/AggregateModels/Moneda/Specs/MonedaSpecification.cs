using System;
using System.Linq.Expressions;
using Domain.Specification;

namespace Domain.AggregateModels.Moneda.Specs
{
    public class MonedaSpecification : SpecificationBase<Moneda>
    {
        public static Expression<Func<Moneda, bool>> ObtenerMonedaActiva
             => x => x.ActivoDesde <= DateTime.UtcNow && x.ActivoHasta >= DateTime.UtcNow;

        public static Expression<Func<Moneda, bool>> ObtenerMonedaInActiva
             => x => x.ActivoDesde >= DateTime.UtcNow || x.ActivoHasta <= DateTime.UtcNow;

        public static Expression<Func<Moneda, bool>> ExisteMonedaPorCodigo(string identificador, int codigo)
        {
            return x => x.Identificador.Valor == identificador && x.Codigo != codigo;
        }

        public static Expression<Func<Moneda, bool>> ExisteMonedaPorNombre(string nombre, int codigo)
        {
            return x => x.Nombre.Valor == nombre && x.Codigo != codigo;
        }
    }
}

