namespace Application.Features.Services
{
    using System;
    using MediatR;
    using Application.Models.Validators;
    using Domain.AggregateModels;

    /// <summary>
    /// Class represents the business for the Entity (Menu)
    /// </summary>
    //[BusinessAttribute]
    public partial class MenuService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Menu>,
        Interfaces.IMenuService
    {
        /// <summary>
        /// Constructor to initialize the data access layer, Context Instance [Menu].
        /// </summary>
        /// <param name="repositoryContext">Database context instance</param>
        public MenuService(Domain.Repositories.Interfaces.IMenuRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.MenuDto.Path);
           CreateMapperExpresion<Application.Features.Models.Dto.MenuDto, Domain.AggregateModels.Menu>(cnf => {
               MenuMapper.Expresion(cnf);
           });
        }

    }
}
