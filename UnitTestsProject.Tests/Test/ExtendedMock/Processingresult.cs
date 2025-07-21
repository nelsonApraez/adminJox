namespace UnitTestsProject.Tests
{
    using UnitTestsProject.Tests.Test.MockData.Dtos;

    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Processingresult]
    /// </summary>
    public partial class Processingresult
    {
        public override void BuildMockData()
        {
             //Arrange
             //objetos base para Unit Test
             objDtoInitExits = ProcessingresultDtoMockData.BuildProcessingresultDto();
             objDtoInitExitNotExist = ProcessingresultDtoMockData.BuildProcessingresultDto();

             //Se crea el mock para el repositorio
             adaptaterMain = new Infrastructure.Repositories.ProcessingresultRepository(adaptaterBase.Object);

             //inclusion de mock de repositorios base
             adaptaterBase
                  .AddAdapterBaseMock()
                  .AddProcessingresultMockData();

             //creacion de contenedor Mediatr
             CreateContainerMediatrDm(cfg =>
             {
                  //servicios de aplicacion
                  cfg.AddProcessingresultService();
             });
        }

        public override Domain.Repositories.Interfaces.IProcessingresultRepository GetRepository() => (Domain.Repositories.Interfaces.IProcessingresultRepository)adaptaterMain;

    }
}
