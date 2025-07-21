namespace UnitTestsProject.Tests
{
    using UnitTestsProject.Tests.Test.MockData.Dtos;

    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Pregunta]
    /// </summary>
    public partial class Pregunta
    {
        public override void BuildMockData()
        {
             //Arrange
             //objetos base para Unit Test
             objDtoInitExits = PreguntaDtoMockData.BuildPreguntaDto();
             objDtoInitExitNotExist = PreguntaDtoMockData.BuildPreguntaDto();

             //Se crea el mock para el repositorio
             adaptaterMain = new Infrastructure.Repositories.PreguntaRepository(adaptaterBase.Object);

             //inclusion de mock de repositorios base
             adaptaterBase
                  .AddAdapterBaseMock()
                  .AddPreguntaMockData();

             //creacion de contenedor Mediatr
             CreateContainerMediatrDm(cfg =>
             {
                  //servicios de aplicacion
                  cfg.AddPreguntaService();
             });
        }

        public override Domain.Repositories.Interfaces.IPreguntaRepository GetRepository() => (Domain.Repositories.Interfaces.IPreguntaRepository)adaptaterMain;

    }
}
