using Application.Models.Validators;
using FluentValidation;
using StructureMap;
using UnitTestsProject.Tests.Test.MockData.Entities;

namespace UnitTestsProject.Tests
{
    public static class MonedaExtensions
    {

        public static BaseMockData AddMonedaMockData(this BaseMockData adaptater)
        {
            return adaptater.AddModelMockData(MonedaMockData.GetList())
                            .AddModelDbContextMockData(MonedaMockData.GetList());
        }

        public static ConfigurationExpression AddMonedaService(this ConfigurationExpression cfg)
        {
            cfg.For(typeof(IValidator<Application.Features.Models.Dto.MonedaDto>))
                    .Add(typeof(MonedaValidator));
            cfg.For(typeof(Application.Features.Interfaces.IMonedaService))
               .Add(typeof(Application.Features.Services.MonedaService));

            cfg.For(typeof(Domain.Repositories.Interfaces.IMonedaRepository))
             .Add(typeof(Infrastructure.Repositories.MonedaRepository));

            return cfg;
        }
    }
}
