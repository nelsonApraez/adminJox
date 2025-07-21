using Domain.AggregateModels;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Domain.Repositories.Interfaces
{
    /// <summary>
    /// Interfaz representa las Implementaciones De la Dao para la Entidad (Respuesta)
    /// </summary>
    public partial interface IRespuestaRepository :
        Common.IRepositoryBase<Domain.AggregateModels.Respuesta> 
    {
        public Task<List<Respuesta>> AnswersByProject(Guid idProyecto);
    }

}
