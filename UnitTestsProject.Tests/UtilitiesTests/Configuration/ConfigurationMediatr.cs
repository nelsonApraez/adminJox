using System;
using System.Collections.Generic;
using Application.BaseApplicationHelper;
using Application.Common.Commands;
using Application.Features.Commands;
using Application.Features.Parametro.Command;
using Application.Features.Queries;
using DigiToolsMessage.TextMessageApplication;
using EventSourcingCore;
using EventSourcingCore.Commands;
using EventSourcingCore.Handlers;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Package.Utilities.Net;
using Package.Utilities.Net.Telemetry;
using StructureMap;
using UnitTestsProject.Tests.Components;


namespace UnitTestsProject.Tests
{
    public static class MediatrTestExtensions
    {

        /// <summary>
        /// configuracion de servicios transversales
        /// </summary>
        /// <param name="cfg"></param>
        /// <param name="withSecurity"></param>
        /// <returns></returns>
        public static ConfigurationExpression ConfigurationExpressionBase(this ConfigurationExpression cfg)
        {
            cfg.Scan(scanner =>
            {
                scanner.AssemblyContainingType(typeof(Application.Features.Models.Dto.MonedaDto));
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
                scanner.AssemblyContainingType<IMediator>();
                scanner.AssemblyContainingType<IServiceCollection>();
                scanner.WithDefaultConventions();
                scanner.AddAllTypesOf(typeof(IRequestHandler<,>));
            });

            cfg.For<IMediator>().Use<Mediator>();
            //cfg.For<ServiceFactory>().Use<ServiceFactory>(ctx => ctx.GetInstance);
            //cfg.For<INotificationPublisher>().Use(ctx =>  ctx.GetInstance<ForeachAwaitPublisher>() );



            cfg.For(typeof(ITelemetryApplication))
            .Add(typeof(UtilitiesTests.TelemetryApplicationMock));

            cfg.For(typeof(IEventBusinessPublished))
            .Add(typeof(UtilitiesTests.PublishedEventsMock));


            cfg.For(typeof(IHttpContextAccessor))
            .Add(typeof(HttpContextAccessorMock));


            cfg.For(typeof(IRequestHandler<TextMessageCommand, ResponseApi>))
            .Add(typeof(TextMessageHandler));


            cfg.For(typeof(IRequestHandler<EventPublishedAsync, string>))
              .Add(typeof(EventPublishedAsyncHandler));

            cfg.For(typeof(IRequestHandler<PublishEventDomainCommand, Unit>))
              .Add(typeof(PublishEventDomainHandler));

            cfg.For(typeof(Domain.Services.ICatalogoMensajeService))
            .Add(typeof(DigiToolsMessage.TextMessageApplication.CatalogoMensajeService));

            cfg.For(typeof(Microsoft.Extensions.Configuration.IConfiguration))
               .Add(typeof(ConfigurationMock));
            //Mock IServiceScopeFactory
            
            var serviceProvider = new Mock<IServiceProvider>();
            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);


            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            cfg.For(typeof(IServiceScopeFactory))
                  .Use(serviceScopeFactory.Object);
            var serviceProviderNoti = new Mock<INotificationPublisher>();
            cfg.For(typeof(INotificationPublisher))
                 .Use(serviceProviderNoti.Object);
            /*
            var serviceProvider = new Mock<IServiceProvider>();
            

            var serviceScope = new Mock<IServiceScope>();
            serviceScope.Setup(x => x.ServiceProvider).Returns(serviceProvider.Object);

            var serviceScopeFactory = new Mock<IServiceScopeFactory>();
            serviceScopeFactory
                .Setup(x => x.CreateScope())
                .Returns(serviceScope.Object);

            serviceProvider
                .Setup(x => x.GetService(typeof(IServiceScopeFactory)))
                .Returns(serviceScopeFactory.Object);

            
            */
            return cfg;
        }

        /// <summary>
        /// Configura los handler de mediatr
        /// </summary>
        /// <typeparam name="DTO"></typeparam>
        /// <typeparam name="ENT"></typeparam>
        /// <typeparam name="Service"></typeparam>
        /// <param name="cfg"></param>
        /// <returns></returns>
        public static ConfigurationExpression ConfigurationExpressionBaseDm<DTO, ENT, Service>(this ConfigurationExpression cfg)
            where DTO : class, new()
            where ENT : class, new()
            where Service : BaseApplicationHelper<ENT>
        {
            cfg.For(typeof(IBaseApplicationHelper<ENT>)).Add(typeof(Service));
            cfg.For(typeof(IRequestHandler<CreateEntityAsyncCommand<DTO, ENT>, int?>)).Add(typeof(CreateEntityAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<EditEntityAsyncCommand<DTO, ENT>, bool?>)).Add(typeof(EditEntityAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<DeleteEntityAsyncCommand<DTO, ENT>, bool?>)).Add(typeof(DeleteEntityAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<GetEntityAsyncById<DTO, ENT>, DTO>)).Add(typeof(GetEntityAsyncByIdHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<GetDataEntityAsync<DTO, ENT>, List<DTO>>)).Add(typeof(GetDataEntityAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<ToListEntityPagedAsync<DTO, ENT>, CustomList<DTO>>)).Add(typeof(ToListEntityPagedAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<ToListEntityAsync<DTO, ENT>, List<DTO>>)).Add(typeof(ToListEntityAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<ValidateRulesEntityAsync<DTO, ENT>, List<ResponseApi>>)).Add(typeof(ValidateRulesEntityAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<ValidateDuplicatedEntityAsync<DTO, ENT>, List<ResponseApi>>)).Add(typeof(ValidateDuplicateddEntityAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<ChangeStateEntityAsyncCommand<DTO, ENT>, List<ResponseApi>>)).Add(typeof(ChangeStateEntityAsyncHandler<DTO, ENT>));
            cfg.For(typeof(IRequestHandler<DeleteListEntitiesAsyncCommand<DTO, ENT>, List<ResponseApi>>)).Add(typeof(DeleteListEntitiesAsyncHandler<DTO, ENT>));
            return cfg;
        }


    }
}
