using Domain.AggregateModels;
using System.Threading.Tasks;
using System;

namespace Domain.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz representa las Implementaciones De la Dao para la Entidad (Processingresult)
    /// </summary>
    public partial interface IProcessingresultRepository :
        Common.IRepositoryBase<Domain.AggregateModels.Processingresult> 
    {
        public Task<Processingresult> ProcessingresultByProject(Guid idProyecto);
    }
}
