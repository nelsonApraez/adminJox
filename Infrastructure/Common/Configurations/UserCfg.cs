namespace Infrastructure.Common.Configurations
{
    using Domain.AggregateModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class represents the configuration of the entity (User) 
    /// </summary>
    public class UserCfg : IEntityTypeConfiguration<User>
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (User)
        /// </summary>
        /// <param name="builder">Entity builder</param>
        public void Configure(EntityTypeBuilder<User> builder)  
        {  
            builder.ToTable("User", t => t.ExcludeFromMigrations());   

            builder.HasKey(x => x.Id);


            builder.OwnsOne(p => p.FullName)
                .Property(p => p.Valor )
                .HasColumnName("FullName");
        } 
    }
}
