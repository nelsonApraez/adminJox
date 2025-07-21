namespace Package.DataAzureStorage.Common.Interface
{
    using Microsoft.Azure.Cosmos.Table;
    using System.Collections.Generic;

    /// <summary>
    /// Interfaz TableStorage que permitira la Inyeccion y Polimorfismo de la Dependencias de Storage Account [Table]
    /// </summary>
    public interface IAzureStorage<T>
        where T : class, ITableEntity, new()
    {
        /// <summary>
        /// Se encarga de guardar la información en azure storage.
        /// </summary>
        /// <param name="entityStorage">Contiene la información a guardar.</param>
        /// <returns>Verdadero o falso si la información se guardo exitosamente.</returns>
        bool InsertStorage(T entityStorage);

        /// <summary>
        /// Se encarga de consultar los registros de acuerdo  a la partitionKey por la cual queremos filtrar.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <param name="partitionKey">Contiene la información del campo partitionKey de la tabla de Azure Storage.</param>
        /// <returns>Retorna todos los registros que se encuentran el la tabla de Azure Storage.</returns>
        public IEnumerable<T> RetrieveRecordPartitionKey(string partitionKey, int numberRecord);

        /// <summary>
        /// Se encarga de consultar los registros de acuerdo a la RowKey por la cual queremos filtrar.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <param name="rowKey">Contiene la información del campo partitionKey de la tabla de Azure Storage.</param>
        /// <param name="numberRecord">Total de registros a retornar</param>
        /// <returns>Retorna todos los registros que se encuentran el la tabla de Azure Storage.</returns>
        public IEnumerable<T> RetrieveRecordRowKey(string rowKey, int numberRecord);

        /// <summary>
        ///  Se encarga de consultar todos los registros que se encuentran en la tabla de Azure Storage.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <returns>Retorna todos los registros que se encuentran el la tabla de Azure Storage.</returns>
        IEnumerable<T> DisplayTableRetrieveRecord(int numberRecord);
    }
}
