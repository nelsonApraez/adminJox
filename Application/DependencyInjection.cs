using System.Collections.Generic;
using Application.BaseApplicationHelper;
using Application.Features.Commands;
using Application.Features.Interfaces;
using Application.Features.Models.Dto;
using Application.Features.Queries;
using Application.Features.Services;
using Application.Models.Validators;
using Domain.AggregateModels;
using Domain.AggregateModels.Moneda;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;
using Package.Utilities.Net;


namespace Application
{
    public static partial class DependencyInjection
    {
        /// <summary>
        /// Se encarga de agregar la Inyeccion de Dependencias y Recursos para el uso del patron Mediator.
        /// </summary>
        /// <param name="services"></param>
        public static IServiceCollection AddMediatrDependencyInjection(this IServiceCollection services)
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MonedaDto).Assembly));

            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(ProyectoDto).Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(PreguntaDto).Assembly));
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(RespuestaDto).Assembly));


            //registro de CQRS abstractos de los componentes de negocio
            //services.RegisterMediatrAbstractService<ProyectoService, ProyectoDto, Proyecto, IProyectoService>();
            //services.RegisterMediatrAbstractService<PreguntaService, PreguntaDto, Pregunta, IPreguntaService>();
            //services.RegisterMediatrAbstractService<RespuestaService, RespuestaDto, Respuesta, IRespuestaService>();

            services.RegisterMediatrAbstractService<MonedaService, MonedaDto, Moneda, IMonedaService>();
            services.RegisterMediatrAbstractService<RoleService, RoleDto, Role, IRoleService>();
            services.RegisterMediatrAbstractService<MenuService, MenuDto, Menu, IMenuService>();
            services.RegisterMediatrAbstractService<EntityService, EntityDto, Entity, IEntityService>();
            services.RegisterMediatrAbstractService<UserService, UserDto, User, IUserService>();
            services.RegisterMediatrAbstractService<AuthorizationPermissionsService, AuthorizationPermissionsDto, AuthorizationPermissions, IAuthorizationPermissionsService>();

            // Validator registration is done only once
            services.AddValidatorsFromAssemblyContaining<RoleValidador>();
            services.AddValidatorsFromAssemblyContaining<MenuValidador>();
            services.AddValidatorsFromAssemblyContaining<EntityValidador>();
            services.AddValidatorsFromAssemblyContaining<UserValidador>();
            services.AddValidatorsFromAssemblyContaining<AuthorizationPermissionsValidador>();


            //registro de validator se hace solo una vez
            services.AddValidatorsFromAssemblyContaining<MonedaValidator>();

            //services.AddValidatorsFromAssemblyContaining<ProyectoValidador>();
            //services.AddValidatorsFromAssemblyContaining<PreguntaValidador>();
            //services.AddValidatorsFromAssemblyContaining<RespuestaValidador>();

            //ProcessEngineApplication
            //services.AddScoped(typeof(IRequestHandler<ExecuteTrace, string>), typeof(TraceHandler));
            //services.AddScoped(typeof(IRequestHandler<ExecuteLog, string>), typeof(LogHandler));            


            services.AddTransient<Common.Interfaces.IApplicationEventService, Common.Services.ApplicationEventService>();
            services.AddMediatrDependencyInjectionApp();
            return services;
        }


        /// <summary>
        /// Se encarga de agregar la Inyeccion de Dependencias y Recursos para el uso del patron Mediator para cada comando y query
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <typeparam name="TImplementacion"></typeparam>
        /// <param name="services"></param>
        public static void RegisterMediatrAbstractService<Service, DTO, ENT, TImplementacion>(this IServiceCollection services)
            where Service : BaseApplicationHelper<ENT>
            where DTO : class, new()
            where ENT : class, new()
             where TImplementacion : BaseApplicationHelper.IBaseApplicationHelper<ENT>
        {
            services.AddScoped(typeof(TImplementacion), typeof(Service));
            services.AddScoped(typeof(BaseApplicationHelper.IBaseApplicationHelper<ENT>), typeof(Service));
            services.AddScoped(typeof(IRequestHandler<CreateEntityAsyncCommand<DTO, ENT>, int?>), typeof(CreateEntityAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<EditEntityAsyncCommand<DTO, ENT>, bool?>), typeof(EditEntityAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<DeleteEntityAsyncCommand<DTO, ENT>, bool?>), typeof(DeleteEntityAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<GetEntityAsyncById<DTO, ENT>, DTO>), typeof(GetEntityAsyncByIdHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<GetDataEntityAsync<DTO, ENT>, List<DTO>>), typeof(GetDataEntityAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<ToListEntityPagedAsync<DTO, ENT>, CustomList<DTO>>), typeof(ToListEntityPagedAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<ToListEntityAsync<DTO, ENT>, List<DTO>>), typeof(ToListEntityAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<ValidateRulesEntityAsync<DTO, ENT>, List<ResponseApi>>), typeof(ValidateRulesEntityAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<ChangeStateEntityAsyncCommand<DTO, ENT>, List<ResponseApi>>), typeof(ChangeStateEntityAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<DeleteListEntitiesAsyncCommand<DTO, ENT>, List<ResponseApi>>), typeof(DeleteListEntitiesAsyncHandler<DTO, ENT>));
            services.AddScoped(typeof(IRequestHandler<ValidateDuplicatedEntityAsync<DTO, ENT>, List<ResponseApi>>), typeof(ValidateDuplicateddEntityAsyncHandler<DTO, ENT>));



        }

    }
}
