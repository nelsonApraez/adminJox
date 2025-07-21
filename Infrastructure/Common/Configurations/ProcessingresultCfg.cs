using Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Common.Configurations
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Processingresult)
    /// </summary>
    public class ProcessingresultCfg : IEntityTypeConfiguration<Processingresult>
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Processingresult)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public void Configure(EntityTypeBuilder<Processingresult> builder)  
        {  
              builder.ToTable("Processingresult", "dbo");   

              builder.HasKey(x => x.Id);

                      builder.HasOne(d => d.ProyectoidNavigation)
                            .WithMany()
                            .HasForeignKey(d => d.Proyectoid)
                            .OnDelete(DeleteBehavior.ClientSetNull)
                            .HasConstraintName("FK_Processingresult_Proyectoid_Proyecto");

              builder.OwnsOne(p => p.Suggestedsolution)
                            .Property(p => p.Valor)
                            .HasColumnName("Suggestedsolution");

              builder.OwnsOne(p => p.Benefitcalculation)
                            .Property(p => p.Valor)
                            .HasColumnName("Benefitcalculation");

              builder.OwnsOne(p => p.Accompanyingstrategy)
                            .Property(p => p.Valor)
                            .HasColumnName("Accompanyingstrategy");

   
        } 
    }
}
