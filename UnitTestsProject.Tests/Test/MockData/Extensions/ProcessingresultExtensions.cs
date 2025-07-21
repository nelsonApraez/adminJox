namespace UnitTestsProject.Tests
{
    using Application.Models.Validators;
    using FluentValidation;
    using StructureMap;
    using UnitTestsProject.Tests.Test.MockData.Entities;
    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Processingresult]
    /// </summary>
    public static class ProcessingresultExtensions
    {

        public static BaseMockData AddProcessingresultMockData(this BaseMockData adaptater)
        {
            return adaptater.AddModelMockData(ProcessingresultMockData.GetList()).AddModelDbContextMockData(ProcessingresultMockData.GetList());
        }

        public static ConfigurationExpression AddProcessingresultService(this ConfigurationExpression cfg)
        {

           cfg.For(typeof(IValidator<Application.Features.Models.Dto.ProcessingresultDto>))
              .Add(typeof(ProcessingresultValidador));
           cfg.For(typeof(Application.Features.Interfaces.IProcessingresultService))
              .Add(typeof(Application.Features.Services.ProcessingresultService));
           cfg.For(typeof(Domain.Repositories.Interfaces.IProcessingresultRepository))
              .Add(typeof(Infrastructure.Repositories.ProcessingresultRepository));
           return cfg;
        }

    }
}
