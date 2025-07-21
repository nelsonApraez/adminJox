using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;

namespace Application.Features.Commands
{
    /// <summary>
    /// Patron CQS creacion de repositorios 
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="ENT">Entidad de dominio valida</typeparam>
    public record CreateEntityAsyncCommand<DTO, ENT>(DTO Dto) : IRequest<int?>
       where DTO : class, new()
       where ENT : class, new();

    public class CreateEntityAsyncHandler<DTO, ENT> : IRequestHandler<CreateEntityAsyncCommand<DTO, ENT>, int?>
        where DTO : class, new()
        where ENT : class, new()

    {
        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;

        public CreateEntityAsyncHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }

        public async Task<int?> Handle(CreateEntityAsyncCommand<DTO, ENT> request, CancellationToken cancellationToken)
        {
            var objEntity = _serviceApplication.MapObj<DTO, ENT>(request.Dto);
            return await _serviceApplication.CreateAsync(objEntity).ConfigureAwait(false);
        }


    }

}
