using Application.Models.Respuesta;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace Application.Features.Interfaces
{
    /// <summary>
    /// Esta Interfaz representa las Implementaciones Del Negocio para la Entidad (Respuesta)
    /// </summary>
    public partial interface IRespuestaService :
        BaseApplicationHelper.IBaseApplicationHelper<Domain.AggregateModels.Respuesta> 
    {
        public Task<List<QuestionsWithAnswersDto>> AnswersByProject(Guid idProyecto);
    }
}
