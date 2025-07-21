using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;

namespace Application.Features.Commands
{
    /// <summary>
    /// Patron CQRS eliminacion de repositorios 
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="TImplementacion">Interfaz de negocio que implementa el patron CQRS</typeparam>
    public record DeleteEntityAsyncCommand<DTO, ENT>(DTO Dto) : IRequest<bool?>
        where DTO : class, new()
        where ENT : class, new();

    public class DeleteEntityAsyncHandler<DTO, ENT> : IRequestHandler<DeleteEntityAsyncCommand<DTO, ENT>, bool?>
        where DTO : class, new()
        where ENT : class, new()
    {

        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;

        public DeleteEntityAsyncHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }


        public async Task<bool?> Handle(DeleteEntityAsyncCommand<DTO, ENT> request, CancellationToken cancellationToken)
        {
            var objEntity = _serviceApplication.MapObj<DTO, ENT>(request.Dto);
            return await _serviceApplication.DeleteAsync(objEntity);
        }
    }
}
