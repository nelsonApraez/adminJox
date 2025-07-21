namespace UnitTestsProject.Tests
{
    using Application.Models.Validators;
    using FluentValidation;
    using StructureMap;
    using UnitTestsProject.Tests.Test.MockData.Entities;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Respuesta]
    /// </summary>
    public static class RespuestaExtensions
    {

        public static BaseMockData AddRespuestaMockData(this BaseMockData adaptater)
        {
            return adaptater.AddModelMockData(RespuestaMockData.GetList()).AddModelDbContextMockData(RespuestaMockData.GetList());
        }

        public static ConfigurationExpression AddRespuestaService(this ConfigurationExpression cfg)
        {

           cfg.For(typeof(IValidator<Application.Features.Models.Dto.RespuestaDto>))
              .Add(typeof(RespuestaValidador));
           cfg.For(typeof(Application.Features.Interfaces.IRespuestaService))
              .Add(typeof(Application.Features.Services.RespuestaService));
           cfg.For(typeof(Domain.Repositories.Interfaces.IRespuestaRepository))
              .Add(typeof(Infrastructure.Repositories.RespuestaRepository));
           return cfg;
        }

    }
}
