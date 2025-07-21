namespace Infrastructure.Common.Configurations
{
    using Domain.AggregateModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class represents the configuration of the entity (Entity) 
    /// </summary>
    public class EntityCfg : IEntityTypeConfiguration<Entity>
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (Entity)
        /// </summary>
        /// <param name="builder">Entity builder</param>
        public void Configure(EntityTypeBuilder<Entity> builder)  
        {  
            builder.ToTable("Entity", t => t.ExcludeFromMigrations());   

            builder.HasKey(x => x.Id);

            builder.OwnsOne(p => p.Name)
                .Property(p => p.Valor )
                .HasColumnName("Name");
        } 
    }
}
