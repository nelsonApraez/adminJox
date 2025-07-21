namespace Package.DataAzureStorage.Common
{
    using Package.Utilities.Net;
    using Package.DataAzureStorage.Common.Interface;
    using Microsoft.Azure.Cosmos.Table;
    using Microsoft.Extensions.Options;

    /// <summary>
    /// Clase Contexto/Inicializcion del Storage Account [Table] del proyecto
    /// </summary>
    public class MainContext : IMainContext
    {
        /// <summary>
        /// Refenrencia de la Table
        /// </summary>
        private readonly CloudTableClient cloudTableClient;

        /// <summary>
        /// Contructor para Inicializacion del Storage Account [Table]
        /// </summary>
        /// <param name="configuration">Opciones de Configuracion para la conexion del Storage Account [Table]</param>
        public MainContext(IOptions<StorageSettings> configuration)
        {
            configuration.Value.ConnectionString.IsValidThrow(EnumerationMessage.Message.ErrorStorageConnectionAzureStorage);

            CloudStorageAccount storageAccount = CloudStorageAccount.Parse(configuration.Value.ConnectionString);
            cloudTableClient = storageAccount.CreateCloudTableClient(new TableClientConfiguration());
        }

        /// <summary>
        /// Obtener la tabla de almacenamiento
        /// </summary>
        /// <param name="tableName">Nombre de la tabla</param>
        /// <returns>Instancia de la tabla</returns>
        public CloudTable GetCloudTable(string tableName)
        {
            tableName.IsValidThrow(EnumerationMessage.Message.ErrorTableNameAzureStorage);
            CloudTable cloudTable = cloudTableClient.GetTableReference(tableName);
            cloudTable.CreateIfNotExists();

            return cloudTable;
        }
    }
}
