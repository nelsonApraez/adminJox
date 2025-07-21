using System.Threading.Tasks;
using System;

namespace Application.Features.Interfaces
{
    /// <summary>
    /// Esta Interfaz representa las Implementaciones Del Negocio para la Entidad (Processingresult)
    /// </summary>
    public partial interface IProcessingresultService :
        BaseApplicationHelper.IBaseApplicationHelper<Domain.AggregateModels.Processingresult>
    {
        public Task<Application.Features.Models.Dto.ProcessingresultDto> GetProcessingresultByProject(Guid idProyecto);


        public Task<byte[]>  GetProcessingFileByProject(Guid idProyecto);
    }
}
