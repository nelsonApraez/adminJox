using Package.Utilities.Net;

namespace Api.Installers
{
    public class Cors : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddCors(options =>
            {
                options.AddPolicy(Constants.CorsPolicy, builder => builder
                .WithOrigins(configuration?.GetSection(Constants.ConfigurationApplication + "CorsPolicyOrigins").Value.Split('|'))
                .AllowAnyMethod()
                .AllowAnyHeader()
                .AllowCredentials());
            });
        }
    }
}
