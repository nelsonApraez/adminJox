namespace UnitTestsProject.Tests
{
    using Application.Models.Validators;
    using FluentValidation;
    using StructureMap;
    using UnitTestsProject.Tests.Test.MockData.Entities;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Pregunta]
    /// </summary>
    public static class PreguntaExtensions
    {

        public static BaseMockData AddPreguntaMockData(this BaseMockData adaptater)
        {
            return adaptater.AddModelMockData(PreguntaMockData.GetList()).AddModelDbContextMockData(PreguntaMockData.GetList());
        }

        public static ConfigurationExpression AddPreguntaService(this ConfigurationExpression cfg)
        {

           cfg.For(typeof(IValidator<Application.Features.Models.Dto.PreguntaDto>))
              .Add(typeof(PreguntaValidador));
           cfg.For(typeof(Application.Features.Interfaces.IPreguntaService))
              .Add(typeof(Application.Features.Services.PreguntaService));
           cfg.For(typeof(Domain.Repositories.Interfaces.IPreguntaRepository))
              .Add(typeof(Infrastructure.Repositories.PreguntaRepository));
           return cfg;
        }

    }
}
