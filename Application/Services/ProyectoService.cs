namespace Application.Features.Services
{
    using Package.Utilities.Net;
    using System;
    using MediatR;
    using Application.Models.Validators;

    /// <summary>
    /// Clase representa el negocio para la Entidad (Proyecto)
    /// </summary>
    [BusinessAttribute]
    public partial class ProyectoService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Domain.AggregateModels.Proyecto>,
        Interfaces.IProyectoService
    {
        /// <summary>
        /// Constructor para inicializar la capa de acceso a datos, Instacia del Contexto [Proyecto].
        /// </summary>
        /// <param name="repositoryContext">Instacia del Contexto a Base de Datos</param>
        public ProyectoService(Domain.Repositories.Interfaces.IProyectoRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator) 
        {
           OrderDefaultEntity = nameof(Application.Features.Models.Dto.ProyectoDto.Nombre);
           CreateMapperExpresion<Application.Features.Models.Dto.ProyectoDto, Domain.AggregateModels.Proyecto>(cnf => {
               ProyectoMapper.Expresion(cnf);
           });
        }

    }
}
