using Microsoft.IdentityModel.Tokens;

namespace Api.Installers
{
    public class Authentication : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddAuthentication("Bearer")
               .AddJwtBearer("Bearer", options =>
               {
                   options.Authority = "https://login.microsoftonline.com/5bee910d-30c8-4c9c-81e5-6612b794efd6/v2.0";
                   options.Audience = "api://aa19bab2-c446-4ec9-867b-0a826aa9ee2c";
                   options.TokenValidationParameters.ValidateIssuer = false;

                   options.Authority = configuration.GetSection("AdFs:Authority").Value;
                   options.Audience = configuration.GetSection("AdFs:Audience").Value;
                   options.RequireHttpsMetadata = true;

                   options.TokenValidationParameters = new TokenValidationParameters
                   {
                       ValidIssuer = configuration.GetSection("AdFs:ValidIssuer").Value,
                       ValidAudience = configuration.GetSection("AdFs:Audience").Value,
                       ValidateAudience = true,
                       ValidateIssuer = true,
                       ValidateLifetime = true
                   };

               });

        }
    }
}
