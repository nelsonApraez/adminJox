namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using Package.Utilities.Net;
    using FluentValidation;
    /// <summary>
    /// Esta clase representa la Implementacion AbstractValidator para la Entidad (Prompt)
    /// </summary>
    public class PromptValidador : AbstractValidator<Features.Models.Dto.PromptDto> 
    { 
      public PromptValidador()  
      { 
           RuleFor(x => x.Nombre).Length(1, 100).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Nombre|100 {Constants.CharMaxLength}");
           RuleFor(x => x.Promtuser).Length(1, 10000).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Promtuser|10000 {Constants.CharMaxLength}");
           RuleFor(x => x.Promtsystem).Length(1, 10000).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Promtsystem|10000 {Constants.CharMaxLength}");
           RuleFor(x => x.Tags).Length(1, 100).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Tags|100 {Constants.CharMaxLength}");
           RuleFor(x => x.Folder).Length(1, 100).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Folder|100 {Constants.CharMaxLength}");
 
      }

    }

    public static class PromptMapper
    {

      public static void Expresion(IMapperConfigurationExpression cnf)
      {
       if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Prompt)))  
         return;
        cnf.CreateMap<Features.Models.Dto.PromptDto, Domain.AggregateModels.Prompt>().ReverseMap();
 
      }

    }

}
