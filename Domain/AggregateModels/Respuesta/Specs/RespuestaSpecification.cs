using System;
using System.Linq.Expressions;
using Domain.Specification;

namespace Domain.AggregateModels.Specs
{
    public class RespuestaSpecification : SpecificationBase<Respuesta>
    {
        public static Expression<Func<Respuesta, bool>> AnwersByIdProyect(Guid idProyecto)
        {
            return x => x.Proyectoid == idProyecto;
        }
    }
}
