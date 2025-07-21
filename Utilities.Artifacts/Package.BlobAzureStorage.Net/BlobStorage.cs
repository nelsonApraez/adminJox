namespace BlobAzureStorage
{
    using Azure.Storage.Blobs;
    using Azure.Storage.Blobs.Specialized;
    using Azure.Storage.Sas;
    using BlobAzureStorage.Common.Interface;
    using System;
    using System.IO;
    using System.Threading.Tasks;

    /// <summary>
    /// Clase que se encarga de hacer las operaciones de Azure Storage Account [Blob]
    /// </summary>
    public class BlobStorage : IBlobStorage
    {
        /// <summary>
        /// Contiene el contexto del Contenedor
        /// </summary>
        private readonly BlobContainerClient blobContainerClient;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="mainContext"></param>
        protected BlobStorage(IMainContext mainContext)
        {
            this.blobContainerClient = mainContext.GetCloudContainer();
        }

        /// <summary>
        /// Descargar Archivo del Contenedor
        /// </summary>
        /// <param name="fileName">Nombre del Archivo</param>
        /// <returns>Archivo</returns>
        public async Task<byte[]> DowloadFileAsync(string fileName)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            var downloadFilePath = await blobClient.DownloadAsync();

            byte[] file;
            using (var stream = new MemoryStream())
            {
                await downloadFilePath.Value.Content.CopyToAsync(stream);
                file = stream.ToArray();
            }

            return file;
        }

        /// <summary>
        /// Obtener el Token SAS del archivo <paramref name="fileName"/>
        /// </summary>
        /// <param name="fileName">Nombre del Archivo</param>
        /// <param name="durationTimeMinutes">Duración del Token SAS en Minutos</param>
        /// <returns>Url del archivo con su token SAS para la descarga</returns>
        public Uri GetToken(string fileName, int durationTimeMinutes)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            if (blobClient.CanGenerateSasUri)
            {
                BlobSasBuilder sasBuilder = new BlobSasBuilder()
                {
                    BlobContainerName = blobClient.GetParentBlobContainerClient().Name,
                    BlobName = blobClient.Name,
                    Resource = "b"
                };

                sasBuilder.ExpiresOn = DateTimeOffset.UtcNow.AddMinutes(durationTimeMinutes);
                sasBuilder.SetPermissions(BlobSasPermissions.Read);

                Uri sasUri = blobClient.GenerateSasUri(sasBuilder);
                return sasUri;
            }

            return null;
        }

        /// <summary>
        /// Cargar Archivo en el Contenedor
        /// </summary>
        /// <param name="file">Archivo</param>
        /// <param name="fileName">Nombre del Archivo</param>
        /// <param name="overwrite">Indicar si se sobreescribira en el contenedor</param>
        /// <returns>Url del archivo cargado</returns>
        public async Task<Uri> UploadFileAsync(byte[] file, string fileName, bool overwrite)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);

            using (var stream = new MemoryStream(file, writable: false))
            {
                await blobClient.UploadAsync(stream, overwrite);
            }

            return blobClient.Uri;
        }

        /// <summary>
        /// Eliminar Archivo del Contenedor
        /// </summary>
        /// <param name="fileName">Nombre del Archivo</param>
        /// <returns>Task de la Accion</returns>
        public async Task DeleteFileAsync(string fileName)
        {
            BlobClient blobClient = blobContainerClient.GetBlobClient(fileName);
            await blobClient.DeleteAsync();
        }
    }
}
