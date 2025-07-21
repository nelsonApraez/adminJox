namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using FluentValidation;
    using Application.Features.Models.Dto;
    using Domain.AggregateModels;
    using Package.Utilities.Net;
    using Domain.AggregateModels.Moneda.Specs;
    using Domain.AggregateModels.Moneda;


    /// <summary>
    /// This class represents the Implementation of the AbstractValidator for the Entity (User)
    /// </summary>
    public class UserValidador : AbstractValidator<UserDto>
    {
        public UserValidador()
        {
            //RuleFor(x => x.Id).NotEmpty().WithErrorCode($"{EnumerationMessage.Message.DatoRequerido}").WithMessage($"Id");
            RuleFor(x => x.Username).NotEmpty().WithErrorCode($"{EnumerationMessage.Message.DatoRequerido}").WithMessage($"Username");
            RuleFor(x => x.Username).Length(1, 250).WithErrorCode($"{EnumerationMessage.Message.Longitud}").WithMessage($"Username|250 {Constants.CharMaxLength}");
            RuleSet($"{EnumerationMessage.Message.Duplicado}", () =>
            {/*

                RuleFor(v => v)
                   .MustAsync(async (x, cancellation) =>
                   {
                       return await _repository.ExistElementAsync(MonedaSpecification.ExisteMonedaPorCodigo(x.Identificador, x.Codigo));
                   }).WithErrorCode($"{EnumerationMessage.Message.Duplicado}.{nameof(Moneda.Identificador)}")
                      .WithName(nameof(Moneda.Identificador));
                */
            });
        }
    }

    public static class UserMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {
            if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.User)))
                return;

            cnf.CreateMap<UserDto, User>()
               .ConstructUsing(s => s != null ?
                    new User(s.Id, s.Username, s.FullName, s.Email, s.EmailConfirmed, s.PasswordHash, s.PhoneNumber, s.RoleId, s.Salt) : null);

            cnf.CreateMap<User, UserDto>()
                .ForMember(dest => dest.FullName,
                  opt => opt.MapFrom(x => x.FullName.Valor))
                .ForMember(dest => dest.PasswordHash, act => act.Ignore())
                .ForMember(dest => dest.Salt, act => act.Ignore());
        }
    }
}
