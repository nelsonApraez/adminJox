namespace Application.Features.Services
{
    using System;
    using System.Threading.Tasks;
    using MediatR;
    using Application.Models.Validators;
    using Domain.AggregateModels;
    using Domain.Repositories.Interfaces;


    /// <summary>
    /// Class represents the business for the Entity (User)
    /// </summary>
    //[BusinessAttribute]
    public partial class UserService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.User>,
        Interfaces.IUserService
    {
        /// <summary>
        /// Constructor to initialize the data access layer, Context Instance [User].
        /// </summary>
        /// <param name="repositoryContext">Database context instance</param>
        public UserService(Domain.Repositories.Interfaces.IUserRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.UserDto.Username);
           CreateMapperExpresion<Application.Features.Models.Dto.UserDto, Domain.AggregateModels.User>(cnf => {
               UserMapper.Expresion(cnf);
           });
        }

        public virtual async Task<User> LoginAsync(User user)
        {
            return await ((IUserRepository)Repository).LoginAsync(user);
            //return await Repository.CreateAsync(user).ConfigureAwait(false);
        }

    }
}
