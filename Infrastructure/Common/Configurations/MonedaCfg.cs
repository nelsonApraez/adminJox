using Domain.AggregateModels.Moneda;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Common.Configurations
{
    public class MonedaCfg : IEntityTypeConfiguration<Moneda>
    {
        public void Configure(EntityTypeBuilder<Moneda> builder)
        {
            builder.ToTable("Moneda", "dbo", t => t.ExcludeFromMigrations())
                .Ignore(x => x.RangoFechas)
                .Ignore(x => x.Estado);

            builder.HasKey(x => x.Codigo);

            builder.OwnsOne(p => p.Identificador)
                            .Property(p => p.Valor)
                            .HasColumnName("Identificador");

            builder.OwnsOne(p => p.Nombre)
                            .Property(p => p.Valor)
                            .HasColumnName("Nombre");

            builder.OwnsOne(p => p.Descripcion)
                            .Property(p => p.Valor)
                            .HasColumnName("Descripcion");

        }
    }
}
