using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;
using Package.Utilities.Net;

namespace Application.Features.Commands
{
    /// <summary>
    /// Eliminar lista de entidaddes de negocio
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="ENT">Interfaz de negocio que implementa el patron CQRS</typeparam>
    public record DeleteListEntitiesAsyncCommand<DTO, ENT>(IList<DTO> ListObj, string PropertyName) : IRequest<List<ResponseApi>>
         where DTO : class, new()
         where ENT : class, new();

    public class DeleteListEntitiesAsyncHandler<DTO, ENT> : IRequestHandler<DeleteListEntitiesAsyncCommand<DTO, ENT>, List<ResponseApi>>
     where DTO : class, new()
          where ENT : class, new()
    {
        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;

        public DeleteListEntitiesAsyncHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }


        public async Task<List<ResponseApi>> Handle(DeleteListEntitiesAsyncCommand<DTO, ENT> request, CancellationToken cancellationToken)
        {
            var objEntity = _serviceApplication.MapObj<IList<DTO>, IList<ENT>>(request.ListObj);
            return await _serviceApplication.DeleteListEntities(objEntity, request.PropertyName);
        }
    }
}
