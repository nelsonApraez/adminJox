namespace UnitTestsProject.Tests
{
    using Application.Models.Validators;
    using FluentValidation;
    using StructureMap;
    using UnitTestsProject.Tests.Test.MockData.Entities;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Proyecto]
    /// </summary>
    public static class ProyectoExtensions
    {

        public static BaseMockData AddProyectoMockData(this BaseMockData adaptater)
        {
            return adaptater.AddModelMockData(ProyectoMockData.GetList()).AddModelDbContextMockData(ProyectoMockData.GetList());
        }

        public static ConfigurationExpression AddProyectoService(this ConfigurationExpression cfg)
        {

           cfg.For(typeof(IValidator<Application.Features.Models.Dto.ProyectoDto>))
              .Add(typeof(ProyectoValidador));
           cfg.For(typeof(Application.Features.Interfaces.IProyectoService))
              .Add(typeof(Application.Features.Services.ProyectoService));
           cfg.For(typeof(Domain.Repositories.Interfaces.IProyectoRepository))
              .Add(typeof(Infrastructure.Repositories.ProyectoRepository));
           return cfg;
        }

    }
}
