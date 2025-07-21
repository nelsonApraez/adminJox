namespace Infrastructure.Common
{
    using System.Threading.Tasks;
    using Microsoft.Data.SqlClient;
    using Microsoft.EntityFrameworkCore;

    /// <summary>
    /// Clase Contexto/Inicializcion y Adicion de Procedimientos Adicionados a La Base de datos del proyecto
    /// </summary>
    public partial class MainContext
    {
        /// <summary>
        /// Execute Query SQL
        /// </summary>
        /// <param name="sQuery">The Query SQL to execute.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteQuery(string sQuery)
        {
            return Database.ExecuteSqlRaw(sQuery);
        }

        /// <summary>
        /// Execute Query SQL
        /// </summary>
        /// <param name="sQuery">The Query SQL to execute.</param>
        /// <param name="sqlParameter">Parameters to use with the SQL query.</param>
        /// <returns>The number of rows affected.</returns>
        public int ExecuteQuery(string sQuery, params SqlParameter[] sqlParameter)
        {
            return Database.ExecuteSqlRaw(sQuery, sqlParameter);
        }

        /// <summary>
        /// Execute Query SQL Async
        /// </summary>
        /// <param name="sQuery">Query execute</param>
        /// <returns>The number of rows affected.</returns>
        public Task<int> ExecuteQueryAsync(string sQuery)
        {
            return Database.ExecuteSqlRawAsync(sQuery);
        }

        /// <summary>
        /// Execute Query SQL Async
        /// </summary>
        /// <param name="sQuery">Query execute</param>
        /// <param name="sqlParameter">List parameters query</param>
        /// <returns>The number of rows affected.</returns>
        public Task<int> ExecuteQueryAsync(string sQuery, params SqlParameter[] sqlParameter)
        {
            return Database.ExecuteSqlRawAsync(sQuery, sqlParameter);
        }
    }
}
