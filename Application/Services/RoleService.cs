namespace Application.Features.Services
{
    using System;
    using MediatR;
    using Application.Models.Validators;
    using Domain.AggregateModels;

    /// <summary>
    /// Class represents the business for the Entity (Role)
    /// </summary>
    //[BusinessAttribute]
    public partial class RoleService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Role>,
        Interfaces.IRoleService
    {
        /// <summary>
        /// Constructor to initialize the data access layer, Context Instance [Role].
        /// </summary>
        /// <param name="repositoryContext">Database context instance</param>
        public RoleService(Domain.Repositories.Interfaces.IRoleRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.RoleDto.Name);
           CreateMapperExpresion<Application.Features.Models.Dto.RoleDto, Domain.AggregateModels.Role>(cnf => {
               RoleMapper.Expresion(cnf);
           });
        }

    }
}
