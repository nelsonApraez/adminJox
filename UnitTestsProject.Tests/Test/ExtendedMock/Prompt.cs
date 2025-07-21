namespace UnitTestsProject.Tests
{
    using UnitTestsProject.Tests.Test.MockData.Dtos;

    /// <summary>
    /// Esta Clase representa las pruebas unitarias del negocio para la Entidad [Prompt]
    /// </summary>
    public partial class Prompt
    {
        public override void BuildMockData()
        {
             //Arrange
             //objetos base para Unit Test
             objDtoInitExits = PromptDtoMockData.BuildPromptDto();
             objDtoInitExitNotExist = PromptDtoMockData.BuildPromptDto();

             //Se crea el mock para el repositorio
             adaptaterMain = new Infrastructure.Repositories.PromptRepository(adaptaterBase.Object);

             //inclusion de mock de repositorios base
             adaptaterBase
                  .AddAdapterBaseMock()
                  .AddPromptMockData();

             //creacion de contenedor Mediatr
             CreateContainerMediatrDm(cfg =>
             {
                  //servicios de aplicacion
                  cfg.AddPromptService();
             });
        }

        public override Domain.Repositories.Interfaces.IPromptRepository GetRepository() => (Domain.Repositories.Interfaces.IPromptRepository)adaptaterMain;

    }
}
