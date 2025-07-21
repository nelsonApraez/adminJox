namespace Infrastructure.Common.Configurations
{
    using Domain.AggregateModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class represents the configuration of the entity (Role) 
    /// </summary>
    public class RoleCfg : IEntityTypeConfiguration<Role>
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (Role)
        /// </summary>
        /// <param name="contexto">Database context instance</param>
        public void Configure(EntityTypeBuilder<Role> builder)  
        {  
            builder.ToTable("Role", t => t.ExcludeFromMigrations());   

            builder.HasKey(x => x.Id);

            builder.OwnsOne(p => p.Name)
                .Property(p => p.Valor )
                .HasColumnName("Name");
        } 
    }
}
