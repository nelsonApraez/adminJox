namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using Package.Utilities.Net;
    using FluentValidation;
    /// <summary>
    /// Esta clase representa la Implementacion AbstractValidator para la Entidad (Processingresult)
    /// </summary>
    public class ProcessingresultValidador : AbstractValidator<Features.Models.Dto.ProcessingresultDto> 
    { 
      public ProcessingresultValidador()  
      { 
           RuleFor(x => x.Proyectoid).NotEmpty().WithErrorCode($"{ EnumerationMessage.Message.DatoRequerido}").WithMessage($"Proyectoid"); 
           RuleFor(x => x.Proyectoid).Length(1, 100).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Proyectoid|100 {Constants.CharMaxLength}");
           RuleFor(x => x.Suggestedsolution).Length(1, 20000).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Suggestedsolution|20000 {Constants.CharMaxLength}");
           RuleFor(x => x.Benefitcalculation).Length(1, 20000).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Benefitcalculation|20000 {Constants.CharMaxLength}");
           RuleFor(x => x.Accompanyingstrategy).Length(1, 20000).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Accompanyingstrategy|20000 {Constants.CharMaxLength}");
 
      }

    }

    public static class ProcessingresultMapper
    {

      public static void Expresion(IMapperConfigurationExpression cnf)
      {
       if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Processingresult)))  
         return;
        cnf.CreateMap<Features.Models.Dto.ProcessingresultDto, Domain.AggregateModels.Processingresult>().ReverseMap();
        if (!((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Proyecto)))
          ProyectoMapper.Expresion(cnf);

 
      }

    }

}
