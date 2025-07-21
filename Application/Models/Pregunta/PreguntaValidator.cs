namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using Package.Utilities.Net;
    using FluentValidation;
    /// <summary>
    /// Esta clase representa la Implementacion AbstractValidator para la Entidad (Pregunta)
    /// </summary>
    public class PreguntaValidador : AbstractValidator<Features.Models.Dto.PreguntaDto> 
    { 
      public PreguntaValidador()  
      { 

           RuleFor(x => x.Valor).Length(1, 255).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Valor|255 {Constants.CharMaxLength}");
           RuleFor(x => x.Descripcion).Length(1, 255).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Descripcion|255 {Constants.CharMaxLength}");
 
      }

    }

    public static class PreguntaMapper
    {

      public static void Expresion(IMapperConfigurationExpression cnf)
      {
       if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Pregunta)))  
         return;
        cnf.CreateMap<Features.Models.Dto.PreguntaDto, Domain.AggregateModels.Pregunta>().ReverseMap();
        if (!((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Proyecto)))
          ProyectoMapper.Expresion(cnf);

 
      }

    }

}
