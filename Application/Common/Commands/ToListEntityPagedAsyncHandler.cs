using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;
using Package.Utilities.Net;

namespace Application.Features.Queries
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="ENT">Interfaz de negocio que implementa el patron CQRS</typeparam>
    public record ToListEntityPagedAsync<DTO, ENT>(ParameterGetList ParameterGetList, Filter ObjFilter) : IRequest<CustomList<DTO>>
      where DTO : class, new()
     where ENT : class, new();


    public class ToListEntityPagedAsyncHandler<DTO, ENT> : IRequestHandler<ToListEntityPagedAsync<DTO, ENT>, CustomList<DTO>>
        where DTO : class, new()
        where ENT : class, new()
    {
        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;
        public ToListEntityPagedAsyncHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }
        public async Task<CustomList<DTO>> Handle(ToListEntityPagedAsync<DTO, ENT> request, CancellationToken cancellationToken)
        {
            var lsEntitys = await _serviceApplication.ToListPaged(request.ParameterGetList, request.ObjFilter);
            return await _serviceApplication.MapObjAsyn<CustomList<ENT>, CustomList<DTO>>(lsEntitys);
        }
    }
}
