using Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Common.Configurations
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Respuesta)
    /// </summary>
    public class RespuestaCfg : IEntityTypeConfiguration<Respuesta>
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Respuesta)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public void Configure(EntityTypeBuilder<Respuesta> builder)  
        {  
              builder.ToTable("Respuesta", "dbo");   

              builder.HasKey(x => x.Id);

            builder.HasOne(d => d.PreguntaidNavigation)
                .WithMany()
                .HasForeignKey(d => d.Preguntaid)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Respuesta_Preguntaid_Pregunta");

            builder.HasOne(d => d.ProyectoidNavigation)
                  .WithMany()
                  .HasForeignKey(d => d.Proyectoid)
                  .OnDelete(DeleteBehavior.ClientSetNull)
                  .HasConstraintName("FK_Respuesta_Proyectoid_Proyecto");

            builder.OwnsOne(p => p.Valor)
                            .Property(p => p.Valor)
                            .HasColumnName("Valor");

   
        } 
    }
}
