namespace Infrastructure.Common.Configurations
{
    using Domain.AggregateModels;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    /// <summary>
    /// Class represents the configuration of the entity (User) 
    /// </summary>
    public class AuthorizationPermissionsCfg : IEntityTypeConfiguration<AuthorizationPermissions>
    {
        /// <summary>
        /// Constructor to initialize Context Instance [MainContext] for Entity (AuthorizationPermissions)
        /// </summary>
        /// <param name="builder">Entity builder</param>
        public void Configure(EntityTypeBuilder<AuthorizationPermissions> builder)  
        {  
            builder.ToTable("AuthorizationPermissions", t => t.ExcludeFromMigrations());   

            builder.HasKey(x => x.Id);
        } 
    }
}
