namespace UnitTestsProject.Tests

{
    using System;
    using Application.BaseApplicationHelper;
    using Domain.Common;
    using StructureMap;


    /// <summary>
    /// Esta Clase representa la base para los Test y se encarga de generar los mock de los objetos
    /// </summary>
    /// <typeparam name="DTO">Clase de implementacion genicas para patron DTO</typeparam>
    /// <typeparam name="ENT">Clase generica que representa entidad de dominio</typeparam>
    public abstract class BaseTest<DTO, ENT, Service> : BaseTestBasic
            where DTO : class, new()
            where ENT : class, new()
            where Service : BaseApplicationHelper<ENT>
    {

        #region Properties

        protected IRepositoryBase<ENT> adaptaterMain;

        protected DTO objDtoInitExits;

        protected DTO objDtoInitExitNotExist;

        protected string PkValue = BaseBuilder.GetGuIdValue("1").ToString();
        #endregion

        public BaseTest() : base()
        {
        }


        /// <summary>
        /// Configuracion de contenedor base de Mediatr
        /// </summary>
        /// <param name="action">Configuracion de servicios</param>
        /// <param name="withSecurity">Si aplica seguridad </param>
        public void CreateContainerMediatrDm(Action<ConfigurationExpression> action)
        {
            CreateContainerMediatr(cfg =>
            {
                //Configura los comandos y querys de mediatr
                cfg.ConfigurationExpressionBaseDm<DTO, ENT, Service>();

                //repository
                cfg.For(typeof(Domain.Common.IMainContext))
                   .Use(adaptaterBase.AdaptaterMock.Object);

                cfg.For(typeof(IRepositoryBase<ENT>))
                  .Use(adaptaterMain);

                action(cfg);
            });
        }

        protected void UseContainerDbContext()
        {
            container.Configure(cfg =>
            {
                cfg.For(typeof(Domain.Common.IMainContext))
                   .Use(adaptaterBase.AdaptaterDbContext);
            }
            );
        }


        public virtual IBaseApplicationHelper<ENT> ServiceAplicationBase => container.GetInstance<IBaseApplicationHelper<ENT>>();

        public virtual IRepositoryBase<ENT> GetRepository() => adaptaterMain;


    }
}
