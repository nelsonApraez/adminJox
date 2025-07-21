namespace Application.Features.Services
{
    using System;
    using MediatR;
    using Application.Models.Validators;

    /// <summary>
    /// Class represents the business for the Entity (Entity)
    /// </summary>
    //[BusinessAttribute]
    public partial class EntityService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Entity>,
        Interfaces.IEntityService
    {
        /// <summary>
        /// Constructor to initialize the data access layer, Context Instance [Entity].
        /// </summary>
        /// <param name="repositoryContext">Database context instance</param>
        public EntityService(Domain.Repositories.Interfaces.IEntityRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.EntityDto.Name);
           CreateMapperExpresion<Application.Features.Models.Dto.EntityDto, Domain.AggregateModels.Entity>(cnf => {
               EntityMapper.Expresion(cnf);
           });
        }

    }
}
