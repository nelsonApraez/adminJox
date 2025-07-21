using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;
using Package.Utilities.Net;

namespace Application.Features.Commands
{
    /// <summary>
    /// Cambio de estado de entidad de negocio 
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="TImplementacion">Interfaz de negocio que implementa el patron CQRS</typeparam>
    public record ChangeStateEntityAsyncCommand<DTO, ENT>(DTO Dto, string PropetyState) : IRequest<List<ResponseApi>>
         where DTO : class, new()
          where ENT : class, new();

    public class ChangeStateEntityAsyncHandler<DTO, ENT> : IRequestHandler<ChangeStateEntityAsyncCommand<DTO, ENT>, List<ResponseApi>>
         where DTO : class, new()
          where ENT : class, new()
    {

        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;

        public ChangeStateEntityAsyncHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }

        public async Task<List<ResponseApi>> Handle(ChangeStateEntityAsyncCommand<DTO, ENT> request, CancellationToken cancellationToken)
        {
            var objEntity = _serviceApplication.MapObj<DTO, ENT>(request.Dto);
            return await _serviceApplication.ChangeStateEntity(objEntity, request.PropetyState);
        }
    }
}
