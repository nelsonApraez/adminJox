namespace Infrastructure.Mongo.Common
{
    using System.Threading;
    using System.Threading.Tasks;
    using Domain.Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.ChangeTracking;
    using Microsoft.Extensions.Options;
    using MongoDB.Driver;

    /// <summary>
    /// Clase Contexto/Inicializcion y Extructura de La Base de datos del proyecto
    /// </summary>
    public class MainContext : IMainContext
    {
        /// <summary>
        /// Refenrencia de la Base de Datos [Mongo]
        /// </summary>
        private readonly IMongoDatabase _dB;

        /// <summary>
        /// Contructor para Inicialización de la conexion a Mongo
        /// </summary>
        /// <param name="configuration">Opciones de Configuración para la conexion de Mongo</param>
        public MainContext(IOptions<MongoSettings> configuration)
        {
            var mongoClient = new MongoClient(configuration.Value.ConnectionString);
            _dB = mongoClient.GetDatabase(configuration.Value.MongoDatabase);
        }

        /// <summary>
        /// Obtener Intancia de la Collection de la Base de Datos
        /// </summary>
        /// <typeparam name="TEntity">Entidad de Negocio</typeparam>
        /// <returns>Retorna la Intancia de la Collection de la Base de Datos</returns>
        public IMongoCollection<TEntity> GetCollection<TEntity>()
        {
            return _dB.GetCollection<TEntity>(typeof(TEntity).Name);
        }

        public EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class
        {
            throw new System.NotImplementedException();
        }

        public int ExecuteQuery(string sQuery)
        {
            throw new System.NotImplementedException();
        }

        public Task<int> ExecuteQueryAsync(string sQuery)
        {
            throw new System.NotImplementedException();
        }
        

        public int SaveChanges()
        {
            return 0;
        }

        public Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return Task.FromResult(0);
        }

        public DbSet<TEntity> Set<TEntity>() where TEntity : class
        {
            throw new System.NotImplementedException();
        }
    }
}
