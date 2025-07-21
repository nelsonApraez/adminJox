using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;

namespace Application.Features.Queries
{
    /// <summary>
    /// Patron CQRS obtiene del repositorio la entidad de negocio por id 
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="ENT">Interfaz de negocio que implementa el patron CQRS</typeparam>
    public record GetEntityAsyncById<DTO, ENT>(string Id) : IRequest<DTO>
      where DTO : class, new()
     where ENT : class, new();


    public class GetEntityAsyncByIdHandler<DTO, ENT> : IRequestHandler<GetEntityAsyncById<DTO, ENT>, DTO>
        where DTO : class, new()
        where ENT : class, new()
    {
        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;
        public GetEntityAsyncByIdHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }


        public async Task<DTO> Handle(GetEntityAsyncById<DTO, ENT> request, CancellationToken cancellationToken)
        {
            return await _serviceApplication.MapObjAsyn<ENT, DTO>(await _serviceApplication.GetByIdAsync(request.Id));
        }
    }
}
