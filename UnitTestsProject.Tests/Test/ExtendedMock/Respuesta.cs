namespace UnitTestsProject.Tests
{
    using UnitTestsProject.Tests.Test.MockData.Dtos;

    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Respuesta]
    /// </summary>
    public partial class Respuesta
    {
        public override void BuildMockData()
        {
             //Arrange
             //objetos base para Unit Test
             objDtoInitExits = RespuestaDtoMockData.BuildRespuestaDto();
             objDtoInitExitNotExist = RespuestaDtoMockData.BuildRespuestaDto();

             //Se crea el mock para el repositorio
             adaptaterMain = new Infrastructure.Repositories.RespuestaRepository(adaptaterBase.Object);

             //inclusion de mock de repositorios base
             adaptaterBase
                  .AddAdapterBaseMock()
                  .AddRespuestaMockData();

             //creacion de contenedor Mediatr
             CreateContainerMediatrDm(cfg =>
             {
                  //servicios de aplicacion
                  cfg.AddRespuestaService();
             });
        }

        public override Domain.Repositories.Interfaces.IRespuestaRepository GetRepository() => (Domain.Repositories.Interfaces.IRespuestaRepository)adaptaterMain;

    }
}
