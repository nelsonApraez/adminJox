namespace Application.Features.Commands
{
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Features.Interfaces;
    using MediatR;

    /// <summary>
    /// CQRS repository creation pattern
    /// </summary>
    /// <typeparam name="UserDto">Abstract discount pattern business class <Application.Features.Models.Dto.Application.Features.Models.Dto./typeparam>
    /// <typeparam name="User">Valid domain entity</typeparam>
    public record LoginAsyncCommand<UserDto, User>(UserDto Dto) : IRequest<UserDto>
       where UserDto : class, new()
       where User : class, new();

    public class LoginAsyncHandler<UserDto, User> : IRequestHandler<LoginAsyncCommand<UserDto, Domain.AggregateModels.User>, UserDto>
        where UserDto : class, new()
        where User : class, new()
    {
        protected readonly IUserService _serviceApplication;

        public LoginAsyncHandler(IUserService implementation)
        {
            _serviceApplication = implementation;
        }

        public async Task<UserDto> Handle(LoginAsyncCommand<UserDto, Domain.AggregateModels.User> request, CancellationToken cancellationToken)
        {
            var objEntity = _serviceApplication.MapObj<UserDto, Domain.AggregateModels.User>(request.Dto);
            return await _serviceApplication.MapObjAsyn<Domain.AggregateModels.User, UserDto>(await _serviceApplication.LoginAsync(objEntity).ConfigureAwait(false));
        }
    }
}
