namespace Infrastructure.Common.Configurations
{
    using Domain.AggregateModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class represents the configuration of the entity (Menu) 
    /// </summary>
    public class MenuCfg : IEntityTypeConfiguration<Menu>
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (Menu)
        /// </summary>
        /// <param name="builder">Entity builder</param>
        public void Configure(EntityTypeBuilder<Menu> builder)  
        {  
            builder.ToTable("Menu", t => t.ExcludeFromMigrations());   

            builder.HasKey(x => x.Id);

            builder.OwnsOne(p => p.Title)
                .Property(p => p.Valor )
                .HasColumnName("Title");
        } 
    }
}
