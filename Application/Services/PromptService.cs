namespace Application.Features.Services
{
    using Package.Utilities.Net;
    using System;
    using MediatR;
    using Application.Models.Validators;

    /// <summary>
    /// Clase representa el negocio para la Entidad (Prompt)
    /// </summary>
    [BusinessAttribute]
    public partial class PromptService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Prompt>,
        Interfaces.IPromptService
    {
        /// <summary>
        /// Constructor para inicializar la capa de acceso a datos, Instacia del Contexto [Prompt].
        /// </summary>
        /// <param name="repositoryContext">Instacia del Contexto a Base de Datos</param>
        public PromptService(Domain.Repositories.Interfaces.IPromptRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.PromptDto.Nombre);
           CreateMapperExpresion<Application.Features.Models.Dto.PromptDto, Domain.AggregateModels.Prompt>(cnf => {
               PromptMapper.Expresion(cnf);
           });
        }

    }
}
