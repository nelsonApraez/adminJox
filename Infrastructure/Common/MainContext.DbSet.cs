using Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Common
{
    public partial class MainContext
    {
        #region DbSets
        public DbSet<Proyecto> Proyectos { get; set; }
        public DbSet<Pregunta> Preguntas { get; set; }
        public DbSet<Respuesta> Respuestas { get; set; }
        #endregion
    }
}
