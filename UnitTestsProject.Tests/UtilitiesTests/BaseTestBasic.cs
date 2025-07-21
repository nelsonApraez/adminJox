namespace UnitTestsProject.Tests
{
    using System;
    using Application;
    using Autofac;
    using Domain.Common;
    using Infrastructure;
    using MediatR;
    using Microsoft.Extensions.DependencyInjection;
    using StructureMap;
    using UnitTestsProject.Tests.Components;

    /// <summary>
    /// Esta Clase representa la base para los Test y se encarga de generar los mock de los objetos
    /// </summary>
    public abstract class BaseTestBasic
    {

        #region Properties
        private protected IMediator _mediator;

        private protected BaseMockData adaptaterBase;

        private protected Container container;
        #endregion

        public BaseTestBasic()
        {
            adaptaterBase = new();
            BuildMockData();
        }

        protected void BuilMemoryDbContext()
        {
            adaptaterBase.AdaptaterDbContext = InMemoryDbContextFactory.GetDbContext(_mediator);
        }
        /// <summary>
        /// Configuracion de contenedor base de Mediatr
        /// </summary>
        /// <param name="action">Configuracion de servicios</param>
        /// <param name="withSecurity">Si aplica seguridad </param>        
        public void CreateContainerMediatr(Action<ConfigurationExpression> action)
        {
            var serviceCollection = new ServiceCollection();
            container = new Container(cfg =>
            {
                //configuracion de servicios transversales
                cfg.ConfigurationExpressionBase();                                
                action(cfg);
                cfg.Populate(serviceCollection);
            });
            _mediator = container.GetInstance<IMediator>();           

        }

        public abstract void BuildMockData();
    }
}
