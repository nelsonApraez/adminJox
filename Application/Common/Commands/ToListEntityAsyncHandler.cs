using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;

namespace Application.Features.Queries
{
    /// <summary>
    /// Patron CQRS obtiene del repositorio las entidades de negocio
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="ENT">Interfaz de negocio que implementa el patron CQRS</typeparam>
    public record ToListEntityAsync<DTO, ENT>() : IRequest<List<DTO>>
      where DTO : class, new()
     where ENT : class, new();


    public class ToListEntityAsyncHandler<DTO, ENT> : IRequestHandler<ToListEntityAsync<DTO, ENT>, List<DTO>>
        where DTO : class, new()
        where ENT : class, new()
    {
        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;
        public ToListEntityAsyncHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }

        public async Task<List<DTO>> Handle(ToListEntityAsync<DTO, ENT> request, CancellationToken cancellationToken)
        {
            return await _serviceApplication.MapObjAsyn<List<ENT>, List<DTO>>(await _serviceApplication.ToListAsync());
        }
    }
}
