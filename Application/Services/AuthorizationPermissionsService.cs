namespace Application.Features.Services
{
    using System;
    using MediatR;
    using Application.Models.Validators;

    /// <summary>
    /// Class represents the business for the Entity (AuthorizationPermissions)
    /// </summary>
    //[BusinessAttribute]
    public partial class AuthorizationPermissionsService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.AuthorizationPermissions>,
        Interfaces.IAuthorizationPermissionsService
    {
        /// <summary>
        /// Constructor to initialize the data access layer, Context Instance [AuthorizationPermissions].
        /// </summary>
        /// <param name="repositoryContext">Database context instance</param>
        public AuthorizationPermissionsService(Domain.Repositories.Interfaces.IAuthorizationPermissionsRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.AuthorizationPermissionsDto.Id);
           CreateMapperExpresion<Application.Features.Models.Dto.AuthorizationPermissionsDto, Domain.AggregateModels.AuthorizationPermissions>(cnf => {
               AuthorizationPermissionsMapper.Expresion(cnf);
           });
        }

    }
}
