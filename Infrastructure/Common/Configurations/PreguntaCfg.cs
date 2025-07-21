using Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Common.Configurations
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Pregunta)
    /// </summary>
    public class PreguntaCfg : IEntityTypeConfiguration<Pregunta>
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Pregunta)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public void Configure(EntityTypeBuilder<Pregunta> builder)
        {
            builder.ToTable("Pregunta", "dbo");

            builder.HasKey(x => x.Id);


            builder.OwnsOne(p => p.Valor)
                          .Property(p => p.Valor)
                          .HasColumnName("Valor");

            builder.OwnsOne(p => p.Descripcion)
                          .Property(p => p.Valor)
                          .HasColumnName("Descripcion");

            builder.OwnsOne(p => p.NombreCategoria)
                          .Property(p => p.Valor)
                          .HasColumnName("NombreCategoria");
        }
    }
}
