namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using Package.Utilities.Net;
    using FluentValidation;
    using Application.Models.Respuesta;

    /// <summary>
    /// Esta clase representa la Implementacion AbstractValidator para la Entidad (Respuesta)
    /// </summary>
    public class RespuestaValidador : AbstractValidator<Features.Models.Dto.RespuestaDto> 
    { 
      public RespuestaValidador()  
      {
            RuleFor(x => x.Proyectoid).NotEmpty().WithErrorCode($"{EnumerationMessage.Message.DatoRequerido}").WithMessage($"Proyectoid");
            RuleFor(x => x.Proyectoid).Length(1, 100).WithErrorCode($"{EnumerationMessage.Message.Longitud}").WithMessage($"Proyectoid|100 {Constants.CharMaxLength}");
            RuleFor(x => x.Preguntaid).NotEmpty().WithErrorCode($"{ EnumerationMessage.Message.DatoRequerido}").WithMessage($"Preguntaid"); 
           RuleFor(x => x.Preguntaid).Length(1, 100).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Preguntaid|100 {Constants.CharMaxLength}");
           RuleFor(x => x.Valor).Length(1, 3000).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Valor|3000 {Constants.CharMaxLength}");
 
      }

    }

    public static class RespuestaMapper
    {

      public static void Expresion(IMapperConfigurationExpression cnf)
      {
       if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Respuesta)))  
         return;
        cnf.CreateMap<Features.Models.Dto.RespuestaDto, Domain.AggregateModels.Respuesta>().ReverseMap();
        if (!((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Pregunta)))
          PreguntaMapper.Expresion(cnf);

            cnf.CreateMap<Domain.AggregateModels.Respuesta, QuestionsWithAnswersDto>()
                    .ForMember(x => x.Repuesta, f => f.MapFrom(y => y.Valor.Valor))
                    .ForMember(x => x.Pregunta, f => f.MapFrom(y => y.PreguntaidNavigation.Valor.Valor))
                    .ForMember(x => x.NumeroCategoria, f => f.MapFrom(y => y.PreguntaidNavigation.NumeroCategoria))
                    .ForMember(x => x.NombreCategoria, f => f.MapFrom(y => y.PreguntaidNavigation.NombreCategoria.Valor))
                    .ForMember(x => x.PreguntaId, f => f.MapFrom(y => y.PreguntaidNavigation.Id))
                    .ForMember(x => x.RepuestaId, f => f.MapFrom(y => y.Id));

      }

    }

}
