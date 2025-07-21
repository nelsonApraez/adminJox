using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;
using Package.Utilities.Net;

namespace Application.Features.Queries
{
    /// <summary>
    /// Patron CQRS obtiene del repositorio las entidades de negocio fintros especificos
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="ENT">Interfaz de negocio que implementa el patron CQRS</typeparam>
    public record GetDataEntityAsync<DTO, ENT>(Filter ObjFilter, int TimezoneOffset = 0) : IRequest<List<DTO>>
      where DTO : class, new()
     where ENT : class, new();


    public class GetDataEntityAsyncHandler<DTO, ENT> : IRequestHandler<GetDataEntityAsync<DTO, ENT>, List<DTO>>
        where DTO : class, new()
        where ENT : class, new()
    {
        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;
        public GetDataEntityAsyncHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }

        public async Task<List<DTO>> Handle(GetDataEntityAsync<DTO, ENT> request, CancellationToken cancellationToken)
        {
            //se valida si se debe convertir la fecha un UTC puntual
            _serviceApplication.CreateDateTimeOffsetMapperExpresion(request.TimezoneOffset);
            return await _serviceApplication.MapObjAsyn<List<ENT>, List<DTO>>(await _serviceApplication.GetDataAsync(request.ObjFilter));
        }
    }
}
