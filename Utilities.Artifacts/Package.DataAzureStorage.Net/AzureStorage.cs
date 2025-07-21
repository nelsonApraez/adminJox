namespace Package.DataAzureStorage
{
    using Package.DataAzureStorage.Common.Interface;
    using Microsoft.Azure.Cosmos.Table;
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;

    /// <summary>
    /// Clase que se encarga de hacer las operaciones de Azure Storage Account [Table]
    /// </summary>
    public abstract class AzureStorage<T> : IAzureStorage<T>
        where T : class, ITableEntity, new()
    {
        /// <summary>
        /// Contiene el contexto de base de datos del proyecto
        /// </summary>
        public readonly CloudTable TableAzureStorage;

        /// <summary>
        /// Columna definida por defecto sobre el TableStorage [partitionKey].
        /// </summary>
        private const string PartitionKey = "PartitionKey";

        /// <summary>
        /// Columna definida por defecto sobre el TableStorage [RowKey].
        /// </summary>
        private const string RowKey = "RowKey";

        /// <summary>
        /// Constructor que inicializa vacio el objeto.
        /// </summary>
        protected AzureStorage(IMainContext mainContext)
        {
            this.TableAzureStorage = mainContext.GetCloudTable(typeof(T).Name);
        }

        /// <summary>
        /// Se encarga de consultar los registros de acuerdo a la RowKey por la cual queremos filtrar.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <param name="rowKey">Contiene la información del campo partitionKey de la tabla de Azure Storage.</param>
        /// <param name="numberRecord">Total de registros a retornar</param>
        /// <returns>Retorna todos los registros que se encuentran el la tabla de Azure Storage.</returns>
        public IEnumerable<T> RetrieveRecordRowKey(string rowKey, int numberRecord)
        {
            TableQuery<T> tableQuery = GetFilterTableQuery(RowKey, QueryComparisons.Equal, rowKey).Take(numberRecord);

            return this.ExecuteQuery(tableQuery);
        }

        /// <summary>
        /// Se encarga de consultar los registros de acuerdo  a la PartitionKey por la cual queremos filtrar.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <param name="partitionKey">Contiene la información del campo partitionKey de la tabla de Azure Storage.</param>
        /// <param name="numberRecord">Total de registros a retornar</param>
        /// <returns>Retorna todos los registros que se encuentran el la tabla de Azure Storage.</returns>
        public IEnumerable<T> RetrieveRecordPartitionKey(string partitionKey, int numberRecord)
        {
            TableQuery<T> tableQuery = GetFilterTableQuery(PartitionKey, QueryComparisons.Equal, partitionKey).Take(numberRecord);

            return this.ExecuteQuery(tableQuery);
        }

        /// <summary>
        ///  Se encarga de consultar todos los registros que se encuentran en la tabla de Azure Storage.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <param name="numberRecord">Total de registros a retornar</param>
        /// <returns>Retorna todos los registros que se encuentran el la tabla de Azure Storage.</returns>
        public IEnumerable<T> DisplayTableRetrieveRecord(int numberRecord)
        {
            TableQuery<T> tableQuery = new TableQuery<T>().Take(numberRecord);

            return this.ExecuteQuery(tableQuery);
        }

        /// <summary>
        /// Se encarga de guardar la información en azure storage.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <param name="entityStorage">Contiene la información a guardar.</param>
        /// <returns>Verdadero o falso si la información se guardo exitosamente.</returns>
        public bool InsertStorage(T entityStorage)
        {
            TableOperation tableOperation = TableOperation.InsertOrMerge(entityStorage);
            TableResult tableResult = this.ExecuteOperationStorage(tableOperation);

            return tableResult.Result != null;
        }

        /// <summary>
        /// Se encarga de obtener el filtro a realizar sobre la tabla de azure Storage.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <param name="filter">el campo por el cual se va a filtrar.</param>
        /// <param name="queryComparisons">el campo para realizar la condición de consulta.</param>
        /// <param name="filterCondition">la condición con la que se va a comparar.</param>
        /// <returns></returns>
        private static TableQuery<T> GetFilterTableQuery(string filter, string queryComparisons, string filterCondition)
        {
            return new TableQuery<T>().Where(TableQuery.GenerateFilterCondition(filter, queryComparisons, filterCondition));
        }

        /// <summary>
        /// Se encarga de ejecutar la operación en la tabla de almacenamiento.
        /// </summary>
        /// <param name="tableOperation">contiene la operación realzada en el azure storage.</param>
        /// <returns>Resultado con la información insertada.</returns>
        public async System.Threading.Tasks.Task DeleteDataStorageAsync(int monthsHistory)
        {
            TableQuery<TableEntity> deleteQuery = new TableQuery<TableEntity>()
                    .Where(
                        TableQuery.GenerateFilterCondition(RowKey,
                        QueryComparisons.LessThan,
                        string.Format(CultureInfo.InvariantCulture, "{0:D19}", DateTime.MaxValue.Ticks - DateTime.UtcNow.AddMonths(-monthsHistory).Ticks))
                    )
                    .Select(new string[] { PartitionKey, RowKey });

            TableContinuationToken continuationToken = null;

            do
            {
                var tableQueryResult = this.TableAzureStorage.ExecuteQuerySegmentedAsync(deleteQuery, continuationToken);

                continuationToken = tableQueryResult.Result.ContinuationToken;

                // Split into chunks of 100 for batching
                List<List<TableEntity>> rowsChunked = tableQueryResult.Result.Select((x, index) => new { Index = index, Value = x })
                    .Where(x => x.Value != null)
                    .GroupBy(x => x.Index / 100)
                    .Select(x => x.Select(v => v.Value).ToList())
                    .ToList();

                // Delete each chunk of 100 in a batch
                foreach (List<TableEntity> rows in rowsChunked)
                {
                    TableBatchOperation tableBatchOperation = new TableBatchOperation();
                    rows.ForEach(x => tableBatchOperation.Add(TableOperation.Delete(x)));

                    await this.TableAzureStorage.ExecuteBatchAsync(tableBatchOperation);
                }
            }
            while (continuationToken != null);
        }

        /// <summary>
        /// Se encarga de ejecutar la operación en la tabla de almacenamiento.
        /// </summary>
        /// <param name="tableOperation">contiene la operación realzada en el azure storage.</param>
        /// <returns>Resultado con la información insertada.</returns>
        private TableResult ExecuteOperationStorage(TableOperation tableOperation)
        {
            return this.TableAzureStorage.Execute(tableOperation);
        }

        /// <summary>
        /// Se encarga de ejecutar las consultas.
        /// </summary>
        /// <typeparam name="T">Entidad que implementa TableEntity.</typeparam>
        /// <param name="tableQuery">Contiene la consulta a realizar en la tabla de Azure Storage.</param>
        /// <returns>La información de acuerdo a la consulta realizada.</returns>
        private IEnumerable<T> ExecuteQuery(TableQuery<T> tableQuery)
        {
            return this.TableAzureStorage.ExecuteQuery(tableQuery);
        }
    }
}
