namespace BlobAzureStorage.Common.Interface
{
    using Azure.Storage.Blobs;

    /// <summary>
    /// Interfaz MainContext que permitira la Inyeccion de Dependencias del Storage Account [Blob]
    /// </summary>
    public interface IMainContext
    {
        /// <summary>
        /// Obtener la Refenrencia del Contenedor [Blob]
        /// </summary>
        /// <returns>Refenrencia del Contenedor [Blob]</returns>
        BlobContainerClient GetCloudContainer();
    }
}
