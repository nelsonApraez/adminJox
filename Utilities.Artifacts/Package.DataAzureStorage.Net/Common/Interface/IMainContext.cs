namespace Package.DataAzureStorage.Common.Interface
{
    using Microsoft.Azure.Cosmos.Table;

    /// <summary>
    /// Interfaz MainContext que permitira la Inyeccion de Dependencias del Storage Account [Table]
    /// </summary>
    public interface IMainContext
    {
        /// <summary>
        /// Obtener la tabla de almacenamiento
        /// </summary>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <returns>Instancia de la tabla</returns>
        CloudTable GetCloudTable(string tableName);
    }
}
