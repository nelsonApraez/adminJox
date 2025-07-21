namespace Application.Features.Services
{
    using Package.Utilities.Net;
    using System;
    using MediatR;
    using Application.Models.Validators;

    /// <summary>
    /// Clase representa el negocio para la Entidad (Conversation)
    /// </summary>
    [BusinessAttribute]
    public partial class ConversationService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Conversation>,
        Interfaces.IConversationService
    {
        /// <summary>
        /// Constructor para inicializar la capa de acceso a datos, Instacia del Contexto [Conversation].
        /// </summary>
        /// <param name="repositoryContext">Instacia del Contexto a Base de Datos</param>
        public ConversationService(Domain.Repositories.Interfaces.IConversationRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.ConversationDto.Sessionid);
           CreateMapperExpresion<Application.Features.Models.Dto.ConversationDto, Domain.AggregateModels.Conversation>(cnf => {
               ConversationMapper.Expresion(cnf);
           });
        }

    }
}
