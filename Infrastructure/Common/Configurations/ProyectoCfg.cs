using Domain.AggregateModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
namespace Infrastructure.Common.Configurations
{
    /// <summary>
    /// Clase representa el acceso a datos para la Entidad (Proyecto)
    /// </summary>
    public class ProyectoCfg : IEntityTypeConfiguration<Proyecto>
    {
        /// <summary>
        /// Constructor para inicializar de Instancia del Contexto [MainContext] para la Entidad (Proyecto)
        /// </summary>
        /// <param name="contexto">Instacia del Contexto a Base de Datos</param>
        public void Configure(EntityTypeBuilder<Proyecto> builder)  
        {  
              builder.ToTable("Proyecto", "dbo");   

              builder.HasKey(x => x.Id);

                      builder.OwnsOne(p => p.Nombre)
                            .Property(p => p.Valor)
                            .HasColumnName("Nombre");

              builder.OwnsOne(p => p.Tecnologias)
                            .Property(p => p.Valor)
                            .HasColumnName("Tecnologias");

              builder.OwnsOne(p => p.Descripcion)
                            .Property(p => p.Valor)
                            .HasColumnName("Descripcion");

   
        } 
    }
}
