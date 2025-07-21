namespace Application.Features.Services
{
    using Application.Features.Models.Dto;
    using Application.Models.Validators;
    using Package.Utilities.Net;
    using MediatR;
    using Domain.AggregateModels.Moneda;

    /// <summary>
    /// Clase representa el negocio para la Entidad (Moneda)
    /// </summary>
    [BusinessAttribute]
    public partial class MonedaService :
        Application.BaseApplicationHelper.BaseApplicationHelper<Moneda>,
        Interfaces.IMonedaService
    {
        /// <summary>
        /// Constructor para inicializar la capa de acceso a datos, Instacia del Contexto [Moneda].
        /// </summary>
        /// <param name="repositoryContext">Instacia del Contexto a Base de Datos</param>
        public MonedaService(Domain.Repositories.Interfaces.IMonedaRepository repositoryContext, IMediator mediator) :
            base(repositoryContext, mediator)
        {
            WithPubEvent = true;
            OrderDefaultEntity = nameof(MonedaDto.Identificador);
            DirectionDefault = EnumerationApplication.Orden.Asc.ToString();
            //DTO Adapter
            CreateMapperExpresion<MonedaDto, Moneda>(cnf => {
                MonedaMapper.Expresion(cnf);
            });
        }
    }
}
