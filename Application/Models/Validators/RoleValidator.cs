namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using FluentValidation;
    using Application.Features.Models.Dto;
    using Domain.AggregateModels;
    using Package.Utilities.Net;


    /// <summary>
    /// This class represents the Implementation of the AbstractValidator for the Entity (Role)
    /// </summary>
    public class RoleValidador : AbstractValidator<RoleDto>
    {
        public RoleValidador()
        {
            //RuleFor(x => x.Id).NotEmpty().WithErrorCode($"{EnumerationMessage.Message.DatoRequerido}").WithMessage($"Id");
            RuleFor(x => x.Name).NotEmpty().WithErrorCode($"{EnumerationMessage.Message.DatoRequerido}").WithMessage($"Name");
            RuleFor(x => x.Name).Length(1, 250).WithErrorCode($"{EnumerationMessage.Message.Longitud}").WithMessage($"Name|250 {Constants.CharMaxLength}");
        }
    }

    public static class RoleMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {
            if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Role)))
                return;

            cnf.CreateMap<RoleDto, Role>()
               .ConstructUsing(s => s != null ?
                    new Role(s.Id, s.Name) : null);

            cnf.CreateMap<Role, RoleDto>()
                .ForMember(dest => dest.Name,
                  opt => opt.MapFrom(x => x.Name.Valor ));
        }
    }
}
