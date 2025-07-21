namespace UnitTestsProject.Tests
{
    using UnitTestsProject.Tests.Test.MockData.Dtos;

    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Moneda]
    /// </summary>
    public partial class Moneda
    {        
        
        public override void BuildMockData()
        {
            //Arrange
            //objetos base para Unit Test
            objDtoInitExits = MonedaDtoMockData.BuildMonedaDto();
            objDtoInitExitNotExist = MonedaDtoMockData.BuildMonedaDto();

            //Se crea el mock para el repositorio
            adaptaterMain = new Infrastructure.Repositories.MonedaRepository(adaptaterBase.Object);
            
            //inclusion de mock de repositorios base
            adaptaterBase
                .AddAdapterBaseMock()
                .AddMonedaMockData();

            //creacion de contenedor Mediatr
            CreateContainerMediatrDm(cfg =>
            {
                //servicios de aplicacion
                cfg.AddMonedaService();
            });
        }

        public override Domain.Repositories.Interfaces.IMonedaRepository GetRepository() => (Domain.Repositories.Interfaces.IMonedaRepository)adaptaterMain;


    }
}
