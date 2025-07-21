namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using Package.Utilities.Net;
    using FluentValidation;
    /// <summary>
    /// Esta clase representa la Implementacion AbstractValidator para la Entidad (Proyecto)
    /// </summary>
    public class ProyectoValidador : AbstractValidator<Features.Models.Dto.ProyectoDto> 
    { 
      public ProyectoValidador()  
      { 
           RuleFor(x => x.Nombre).Length(1, 255).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Nombre|255 {Constants.CharMaxLength}");
           RuleFor(x => x.Tecnologias).Length(1, 255).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Tecnologias|255 {Constants.CharMaxLength}");
           RuleFor(x => x.Descripcion).Length(1, 100).WithErrorCode($"{ EnumerationMessage.Message.Longitud}").WithMessage($"Descripcion|100 {Constants.CharMaxLength}");
 
      }

    }

    public static class ProyectoMapper
    {

      public static void Expresion(IMapperConfigurationExpression cnf)
      {
       if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Proyecto)))  
         return;
        cnf.CreateMap<Features.Models.Dto.ProyectoDto, Domain.AggregateModels.Proyecto>().ReverseMap();
 
      }

    }

}
