namespace Application.Models.Validators
{
    using System.Linq;
    using AutoMapper;
    using FluentValidation;
    using Application.Features.Models.Dto;
    using Domain.AggregateModels;    

    /// <summary>
    /// This class represents the Implementation of the AbstractValidator for the Entity (AuthorizationPermissions)
    /// </summary>
    public class AuthorizationPermissionsValidador : AbstractValidator<AuthorizationPermissionsDto>
    {
        public AuthorizationPermissionsValidador()
        {
            //RuleFor(x => x.Id).NotEmpty().WithErrorCode($"{EnumerationMessage.Message.DatoRequerido}").WithMessage($"Id");
        }
    }

    public static class AuthorizationPermissionsMapper
    {
        public static void Expresion(IMapperConfigurationExpression cnf)
        {
            if (((AutoMapper.IProfileConfiguration)cnf).TypeMapConfigs.Any(x => x.SourceType == typeof(Domain.AggregateModels.AuthorizationPermissions)))
                return;

            cnf.CreateMap<AuthorizationPermissionsDto, AuthorizationPermissions>()
               .ConstructUsing(s => s != null ?
                    new AuthorizationPermissions(s.Id, s.EntityId, s.RoleId, s.PermissionCreate, s.PermissionUpdate, s.PermissionDelete, s.PermissionView, s.PermissionList) : null);

            cnf.CreateMap<AuthorizationPermissions, AuthorizationPermissionsDto>();
        }
    }
}
