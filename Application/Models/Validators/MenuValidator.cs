namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using FluentValidation;
    using Application.Features.Models.Dto;
    using Domain.AggregateModels;
    using Package.Utilities.Net;


    /// <summary>
    /// This class represents the Implementation of the AbstractValidator for the Entity (Menu)
    /// </summary>
    public class MenuValidador : AbstractValidator<MenuDto>
    {
        public MenuValidador()
        {
            //RuleFor(x => x.Id).NotEmpty().WithErrorCode($"{EnumerationMessage.Message.DatoRequerido}").WithMessage($"Id");
            RuleFor(x => x.Path).NotEmpty().WithErrorCode($"{EnumerationMessage.Message.DatoRequerido}").WithMessage($"Path");
            RuleFor(x => x.Path).Length(1, 250).WithErrorCode($"{EnumerationMessage.Message.Longitud}").WithMessage($"Path|250 {Constants.CharMaxLength}");
        }
    }

    public static class MenuMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {
            if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.Menu)))
                return;

            cnf.CreateMap<MenuDto, Menu>()
               .ConstructUsing(s => s != null ?
                    new Menu(s.Id, s.Path, s.Title, s.Icon, s.Class, s.Badge, s.BadgeClass, s.IsExternalLink, s.IsParent, s.MenuId) : null);

            cnf.CreateMap<Menu, MenuDto>();
        }
    }
}
