namespace UnitTestsProject.Tests
{
    using UnitTestsProject.Tests.Test.MockData.Dtos;

    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Proyecto]
    /// </summary>
    public partial class Proyecto
    {
        public override void BuildMockData()
        {
             //Arrange
             //objetos base para Unit Test
             objDtoInitExits = ProyectoDtoMockData.BuildProyectoDto();
             objDtoInitExitNotExist = ProyectoDtoMockData.BuildProyectoDto();

             //Se crea el mock para el repositorio
             adaptaterMain = new Infrastructure.Repositories.ProyectoRepository(adaptaterBase.Object);

             //inclusion de mock de repositorios base
             adaptaterBase
                  .AddAdapterBaseMock()
                  .AddProyectoMockData();

             //creacion de contenedor Mediatr
             CreateContainerMediatrDm(cfg =>
             {
                  //servicios de aplicacion
                  cfg.AddProyectoService();
             });
        }

        public override Domain.Repositories.Interfaces.IProyectoRepository GetRepository() => (Domain.Repositories.Interfaces.IProyectoRepository)adaptaterMain;

    }
}
