using Domain.AggregateModels.Specs;
using Domain.AggregateModels;
using System.Threading.Tasks;
using System;
using System.Linq;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Processingresult)
    /// </summary>
    [Package.Utilities.Net.BusinessDaoAttribute]
    public partial class ProcessingresultRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.Processingresult>,
        Domain.Repositories.Interfaces.IProcessingresultRepository
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Processingresult)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public ProcessingresultRepository(Domain.Common.IMainContext contexto) : base(contexto) { }

        public async Task<Processingresult> ProcessingresultByProject(Guid idProyecto)
        {
            var result = await SearchListAsync(ProcessingresultSpecification.ProcessingresultByIdProject(idProyecto));

            return result.FirstOrDefault();

        }
    }
}
