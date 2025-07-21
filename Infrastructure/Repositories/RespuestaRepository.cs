using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.AggregateModels;
using Domain.AggregateModels.Specs;

namespace Infrastructure.Repositories
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Respuesta)
    /// </summary>
    [Package.Utilities.Net.BusinessDaoAttribute]
    public partial class RespuestaRepository :
        Infrastructure.Common.RepositoryBaseDao<Domain.AggregateModels.Respuesta>,
        Domain.Repositories.Interfaces.IRespuestaRepository
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Respuesta)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public RespuestaRepository(Domain.Common.IMainContext contexto) : base(contexto) { }

        public async Task<List<Respuesta>> AnswersByProject(Guid idProyecto) 
        {
            return await SearchListAsync(RespuestaSpecification.AnwersByIdProyect(idProyecto));
            
        }
    }
}
