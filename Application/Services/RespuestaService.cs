namespace Application.Features.Services
{
    using Package.Utilities.Net;
    using System;
    using MediatR;
    using Application.Models.Validators;
    using System.Threading.Tasks;
    using System.Collections.Generic;
    using Application.Features.Models.Dto;
    using Domain.AggregateModels.Specs;
    using Domain.AggregateModels;
    using Application.Models.Respuesta;

    /// <summary>
    /// Clase representa el negocio para la Entidad (Respuesta)
    /// </summary>
    [BusinessAttribute]
    public partial class RespuestaService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Respuesta>,
        Interfaces.IRespuestaService
    {
        /// <summary>
        /// Constructor para inicializar la capa de acceso a datos, Instacia del Contexto [Respuesta].
        /// </summary>
        /// <param name="repositoryContext">Instacia del Contexto a Base de Datos</param>
        public RespuestaService(Domain.Repositories.Interfaces.IRespuestaRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.RespuestaDto.Preguntaid);
           CreateMapperExpresion<Application.Features.Models.Dto.RespuestaDto, Domain.AggregateModels.Respuesta>(cnf => {
               RespuestaMapper.Expresion(cnf);
           });
        }

        public async Task<List<QuestionsWithAnswersDto>> AnswersByProject(Guid idProyecto)
        {
            var temp = await Repository.SearchListAsync(RespuestaSpecification.AnwersByIdProyect(idProyecto));
            return MapObj<List<Respuesta>,List<QuestionsWithAnswersDto>>(temp);
        }

    }
}
