using Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Common.Configurations
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Prompt)
    /// </summary>
    public class PromptCfg : IEntityTypeConfiguration<Prompt>
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Prompt)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public void Configure(EntityTypeBuilder<Prompt> builder)  
        {  
              builder.ToTable("Prompt", "dbo");   

              builder.HasKey(x => x.Id);

                      builder.OwnsOne(p => p.Nombre)
                            .Property(p => p.Valor)
                            .HasColumnName("Nombre");

              builder.OwnsOne(p => p.Promtuser)
                            .Property(p => p.Valor)
                            .HasColumnName("Promtuser");

              builder.OwnsOne(p => p.Promtsystem)
                            .Property(p => p.Valor)
                            .HasColumnName("Promtsystem");

              builder.OwnsOne(p => p.Tags)
                            .Property(p => p.Valor)
                            .HasColumnName("Tags");

              builder.OwnsOne(p => p.Folder)
                            .Property(p => p.Valor)
                            .HasColumnName("Folder");

   
        } 
    }
}
