namespace Application.Features.Services
{
    using Package.Utilities.Net;
    using System;
    using MediatR;
    using Application.Models.Validators;

    /// <summary>
    /// Clase representa el negocio para la Entidad (Pregunta)
    /// </summary>
    [BusinessAttribute]
    public partial class PreguntaService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Pregunta>,
        Interfaces.IPreguntaService
    {
        /// <summary>
        /// Constructor para inicializar la capa de acceso a datos, Instacia del Contexto [Pregunta].
        /// </summary>
        /// <param name="repositoryContext">Instacia del Contexto a Base de Datos</param>
        public PreguntaService(Domain.Repositories.Interfaces.IPreguntaRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.PreguntaDto.Descripcion);
           CreateMapperExpresion<Application.Features.Models.Dto.PreguntaDto, Domain.AggregateModels.Pregunta>(cnf => {
               PreguntaMapper.Expresion(cnf);
           });
        }

    }
}
