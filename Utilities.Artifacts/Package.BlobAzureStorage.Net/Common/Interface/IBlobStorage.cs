namespace BlobAzureStorage.Common.Interface
{
    using System;
    using System.Threading.Tasks;

    /// <summary>
    /// Interfaz BlobStorage que permitira la Inyeccion y Polimorfismo de la Dependencias de Storage Account [Blob]
    /// </summary>
    public interface IBlobStorage
    {
        /// <summary>
        /// Cargar Archivo en el Contenedor
        /// </summary>
        /// <param name="file">Archivo</param>
        /// <param name="fileName">Nombre del Archivo</param>
        /// <param name="overwrite">Indicar si se sobreescribira en el contenedor</param>
        /// <returns>Url del archivo cargado</returns>
        Task<Uri> UploadFileAsync(byte[] file, string fileName, bool overwrite);

        /// <summary>
        /// Obtener el Token SAS del archivo <paramref name="fileName"/>
        /// </summary>
        /// <param name="fileName">Nombre del Archivo</param>
        /// <param name="durationTimeMinutes">Duración del Token SAS en Minutos</param>
        /// <returns>Url del archivo con su token SAS para la descarga</returns>
        Uri GetToken(string fileName, int durationTimeMinutes);

        /// <summary>
        /// Descargar Archivo del Contenedor
        /// </summary>
        /// <param name="fileName">Nombre del Archivo</param>
        /// <returns>Archivo</returns>
        Task<byte[]> DowloadFileAsync(string fileName);

        /// <summary>
        /// Eliminar Archivo del Contenedor
        /// </summary>
        /// <param name="fileName">Nombre del Archivo</param>
        /// <returns>Task de la Accion</returns>
        Task DeleteFileAsync(string fileName);
    }
}
