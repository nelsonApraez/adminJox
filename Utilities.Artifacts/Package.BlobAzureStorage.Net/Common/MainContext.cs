namespace BlobAzureStorage.Common
{
    using Azure.Storage.Blobs;
    using BlobAzureStorage.Common.Interface;
    using Package.Utilities.Net;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Clase Contexto/Inicializcion del Storage Account [Blob] del proyecto
    /// </summary>
    public class MainContext : IMainContext
    {
        /// <summary>
        /// Refenrencia del Contenedor [Blob]
        /// </summary>
        private readonly BlobContainerClient blobContainerClient;


        /// <summary>
        /// Contructor para Inicializacion del Storage Account [Blob]
        /// </summary>
        /// <param name="configuration">Opciones de Configuracion para la conexion del Storage Account [Blob]</param>
        /// <param name="blobServiceClient"></param>
        public MainContext(IOptions<BlobSettings> configuration, BlobServiceClient blobServiceClient)
        {
            configuration.Value.ConnectionString.IsValidThrow(EnumerationMessage.Message.ErrorStorageConnectionAzureStorage);
            configuration.Value.ContenedorBlob.IsValidThrow(EnumerationMessage.Message.ErrorStorageConnectionAzureStorage);
            blobContainerClient = blobServiceClient.GetBlobContainerClient(configuration.Value.ContenedorBlob);
        }

        /// <summary>
        /// Obtener el contenedor de BlobStorage
        /// </summary>
        /// <returns>Instancia del Contenedor de BlobStorage</returns>
        public BlobContainerClient GetCloudContainer()
        {
            return blobContainerClient;
        }
    }
}
