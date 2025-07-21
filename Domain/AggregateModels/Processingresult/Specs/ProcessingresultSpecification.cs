using System;
using System.Linq.Expressions;
using Domain.Specification;

namespace Domain.AggregateModels.Specs
{
    public class ProcessingresultSpecification : SpecificationBase<Processingresult> //TODO: MUY IMPORTANTE DEJAR EL NAMESPACE ASI: Domain.AggregateModels.Specs
    {
        public static Expression<Func<Processingresult, bool>> ProcessingresultByIdProject(Guid idProyecto)
        {
            return (x => x.Proyectoid == idProyecto);
        }
    }
}
