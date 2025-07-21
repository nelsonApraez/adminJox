using System.Threading;
using System.Threading.Tasks;
using Application.BaseApplicationHelper;
using MediatR;

namespace Application.Features.Commands
{
    /// <summary>
    /// Patron CQS Edicion de repositorios 
    /// </summary>
    /// <typeparam name="DTO">Clase abstracta de negocio de patron Dto<Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="ENT">Entidad de dominio valida</typeparam>
    public record EditEntityAsyncCommand<DTO, ENT>(DTO Dto) : IRequest<bool?>
       where DTO : class, new()
       where ENT : class, new();

    public class EditEntityAsyncHandler<DTO, ENT> : IRequestHandler<EditEntityAsyncCommand<DTO, ENT>, bool?>
        where DTO : class, new()
        where ENT : class, new()

    {
        protected readonly IBaseApplicationHelper<ENT> _serviceApplication;

        public EditEntityAsyncHandler(IBaseApplicationHelper<ENT> implementacion)
        {
            _serviceApplication = implementacion;
        }

        public async Task<bool?> Handle(EditEntityAsyncCommand<DTO, ENT> request, CancellationToken cancellationToken)
        {
            var objEntity = _serviceApplication.MapObj<DTO, ENT>(request.Dto);
            return await _serviceApplication.EditAsync(objEntity).ConfigureAwait(false);
        }
    }
}
