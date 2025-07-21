namespace Infrastructure.Common
{
    using System;
    using System.Linq;
    using System.Threading;
    using System.Threading.Tasks;
    using Application.Common.Commands;
    using Domain.Common;
    using MediatR;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Infrastructure;

    /// <summary>
    /// Clase Contexto/Inicializcion y Extructura de La Base de datos del proyecto
    /// </summary>
    public partial class MainContext : DbContext, IMainContext
    {
        private readonly IMediator _publisher;
        /// <summary>
        /// Contructor Por Default
        /// </summary>
        public MainContext()
        {

        }

        /// <summary>
        /// Sobrecarga del Contructor para recibir DbContextOptions
        /// </summary>
        /// <param name="options">Opciones de Configuracion de la conexion a la base de datos</param>
        public MainContext(DbContextOptions<MainContext> options, IMediator publisher)
            : base(options)
        {
            _publisher = publisher;
            Database.AutoTransactionsEnabled = false;
        }

        /// <summary>
        /// Sobrecarga del Contructor para recibir connectionString
        /// </summary>
        /// <param name="connectionString">Cadena de Conexion a la base de datos</param>
        public MainContext(string connectionString) : base(GetOptions(connectionString, null)) { }

        /// <summary>
        /// Sobrecarga del Contructor para recibir connectionString
        /// </summary>
        /// <param name="connectionString">Cadena de Conexion a la base de datos</param>
        /// <param name="sqlServerOptionsAction">Configuración sqlServer Options</param>
        public MainContext(string connectionString, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction) : base(GetOptions(connectionString, sqlServerOptionsAction)) { }

        /// <summary>
        /// Configuracion de la cadena de conexion
        /// </summary>
        /// <param name="connectionString">Cadena de Conexion a la base de datos</param>
        /// <returns>Opciones de Configuracion de la conexion a la base de datos</returns>
        private static DbContextOptions GetOptions(string connectionString, Action<SqlServerDbContextOptionsBuilder> sqlServerOptionsAction)
        {
            return SqlServerDbContextOptionsExtensions.UseSqlServer(new DbContextOptionsBuilder(), connectionString, sqlServerOptionsAction).Options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        /// 
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");
            modelBuilder.HasDefaultSchema("dbo");
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(MainContext).Assembly);
            base.OnModelCreating(modelBuilder);
        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            var result = await base.SaveChangesAsync(cancellationToken);
            if (_publisher != null)
            {
                var events = ChangeTracker.Entries<IHasDomainEvent>()
                        .Select(x => x.Entity.DomainEvents)
                        .SelectMany(x => x)
                        .Where(domainEvent => !domainEvent.IsPublished)
                        .ToArray();
                //se publican los eventos de dominio
                foreach (var @event in events)
                {
                    @event.IsPublished = true;
                    await _publisher?.Send(new PublishEventDomainCommand(@event), cancellationToken);
                }
            }
            return result;
        }

    }
}
