namespace Domain.Common
{
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;

    /// <summary>
    /// Interfaz MainContext que permitira la Inyeccion de Dependencias de la Base de Datos
    /// </summary>
    public partial interface IMainContext
    {
        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <typeparam name="TEntity">Entidad de Negocio</typeparam>
        /// <returns></returns>
        DbSet<TEntity> Set<TEntity>() where TEntity : class;

        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <typeparam name="TEntity">Entidad de Negocio</typeparam>
        /// <param name="entity"></param>
        /// <returns></returns>
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;


        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <returns></returns>
        int SaveChanges();

        /// <summary>
        /// Metodo representarivo de DbContext para realizar los Mock Test.
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        /// <summary>
        /// Metodo representarivo de Execute Query SQL Async
        /// </summary>
        /// <param name="sQuery">Query execute</param>
        /// <returns>The number of rows affected.</returns>
        int ExecuteQuery(string sQuery);

        /// <summary>
        /// Metodo representarivo de Execute Query SQL Async
        /// </summary>
        /// <param name="sQuery">Query execute</param>
        /// <returns>The number of rows affected.</returns>
        Task<int> ExecuteQueryAsync(string sQuery);
    }
}
