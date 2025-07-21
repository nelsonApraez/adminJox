namespace UnitTestsProject.Tests
{
    using Application.Models.Validators;
    using FluentValidation;
    using StructureMap;
    using UnitTestsProject.Tests.Test.MockData.Entities;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Prompt]
    /// </summary>
    public static class PromptExtensions
    {

        public static BaseMockData AddPromptMockData(this BaseMockData adaptater)
        {
            return adaptater.AddModelMockData(PromptMockData.GetList()).AddModelDbContextMockData(PromptMockData.GetList());
        }

        public static ConfigurationExpression AddPromptService(this ConfigurationExpression cfg)
        {

           cfg.For(typeof(IValidator<Application.Features.Models.Dto.PromptDto>))
              .Add(typeof(PromptValidador));
           cfg.For(typeof(Application.Features.Interfaces.IPromptService))
              .Add(typeof(Application.Features.Services.PromptService));
           cfg.For(typeof(Domain.Repositories.Interfaces.IPromptRepository))
              .Add(typeof(Infrastructure.Repositories.PromptRepository));
           return cfg;
        }

    }
}
