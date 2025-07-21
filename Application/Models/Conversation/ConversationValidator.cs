namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using Package.Utilities.Net;
    using FluentValidation;
    /// <summary>
    /// Esta clase representa la Implementacion AbstractValidator para la Entidad (Conversation)
    /// </summary>
    public class ConversationValidador : AbstractValidator<Features.Models.Dto.ConversationDto> 
    { 
      public ConversationValidador()  
      { 
           RuleFor(x => x.Sessionid).Length(1, 1000).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Sessionid|100 {Constants.CharMaxLength}");
           RuleFor(x => x.Personid).Length(1, 1000).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Personid|100 {Constants.CharMaxLength}");           
           RuleFor(x => x.Ragtext).Length(1, 2555).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Ragtext|255 {Constants.CharMaxLength}");
           RuleFor(x => x.Typeoperation).Length(1, 2555).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Typeoperation|255 {Constants.CharMaxLength}");
           RuleFor(x => x.Usertext).Length(1, 2555).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Usertext|255 {Constants.CharMaxLength}");
           RuleFor(x => x.Scoredintent).Length(1, 2555).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Scoredintent|255 {Constants.CharMaxLength}");
           RuleFor(x => x.Typechannel).Length(1, 2555).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Typechannel|255 {Constants.CharMaxLength}");
           RuleFor(x => x.Calification).Length(1, 2555).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Calification|255 {Constants.CharMaxLength}");
 
      }

    }

    public static class ConversationMapper
    {

      public static void Expresion(IMapperConfigurationExpression cnf)
      {
       if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Conversation)))  
         return;
        cnf.CreateMap<Features.Models.Dto.ConversationDto, Domain.AggregateModels.Conversation>().ReverseMap();
 
      }

    }

}
