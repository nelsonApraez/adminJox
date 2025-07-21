using Api.Installers;
using Application;
using Application.Services;
using Infrastructure;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.OpenApi.Models;
using Nico.Assistant;
using Nico.Assistant.Interface;
using Nico.Assistant.Socket;
using Package.Utilities.Net;
using Swashbuckle.AspNetCore.SwaggerUI;
using Web.Api.Base.Api;

namespace Api
{
    /// <summary>
    /// 
    /// </summary>
    public partial class Startup
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="configuration"></param>
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            SwaggerConfiguration = new SwaggerConfiguration();
            configuration.Bind(Constants.SwaggerConfiguration, SwaggerConfiguration);
        }
        protected IConfiguration Configuration { get; }
        protected SwaggerConfiguration SwaggerConfiguration { get; }

        /// <summary>
        /// This method gets called by the runtime. Use this method to add services to the container.
        /// </summary>
        /// <param name="services"></param>
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddResponseCompression();
            services.AddHttpContextAccessor();

            // install all services configurations
            services.InstallServicesInAssembly(Configuration);

            //services.AddScoped<IHttpContextAccessor, HttpContextAccessor>();
            services.AddScoped<IConfigurationApplication, ConfigurationApplication>();

            //Infrastructure DI
            services.AddInfrastructureDenpendencyInjection(Configuration);

            //Configuración Controller:
            services.AddSingleton<SwaggerConfiguration>(SwaggerConfiguration);

            services.AddControllers()
                    .AddJsonOptions(options => options.JsonSerializerOptions.MaxDepth = int.MaxValue)
                    .ConfigureApiBehaviorOptions(option =>
                    {
                        option.InvalidModelStateResponseFactory = actionContext
                                                => ResponseModalStateInvalid.GetInvalidModalState(actionContext);
                    });
            //Application DI
            services.AddHttpClient();
            //services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(Startup).Assembly));
            services.AddMediatrDependencyInjection();

            //Aplication DI NICO
            services.AddTransient<IProcessOperation, ProcessOperation>();
            services.AddNicoDenpendencyInjection(Configuration);
            services.AddSignalR();
        }


        /// <summary>
        /// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        /// </summary>
        /// <param name="app"></param>
        /// <param name="env"></param>
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler(errorApp =>
                {
                    errorApp.Run(async context =>
                    {
                        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
                        context.Response.ContentType = Constants.ContentType;

                        var exceptionHandlerPathFeature =
                            context.Features.Get<IExceptionHandlerPathFeature>();

                        var responseApi = new ResponseApi(EnumerationMessage.Message.ErrorGeneral,
                                                          null,
                                                          exceptionHandlerPathFeature?.Error.Message,
                                                          EnumerationApplication.TypeMessage.Error);
                        await context.Response.WriteAsync(responseApi.ToJsonSerialize());
                    });
                });
                app.UseHsts();
            }
            app.UseRouting();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHub<ChatHubNico>("/helpdesk");
            });

            app.UseResponseCompression();

            app.UseCors(Constants.CorsPolicy);

            app.UseHttpsRedirection();


            app.UseHealthChecks(SwaggerConfiguration.EndpointDescription);
            app.UseStaticFiles();

            app.UseMiddleware<Middleware.CustomMiddleware>();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseSwagger(c =>
            {
                c.PreSerializeFilters.Add((swagger, httpReq) =>
                {
                    swagger.Servers = new List<OpenApiServer> { new() { Url = $"https://{httpReq.Host.Value}{SwaggerConfiguration.DocInfoTitle}" },
                    new() { Url = $"http://{httpReq.Host.Value}{SwaggerConfiguration.DocInfoTitle}" }};
                });
            });
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint(SwaggerConfiguration.EndpointSwaggerJson, SwaggerConfiguration.EndpointDescription);
                options.DocExpansion(DocExpansion.None);
            });
            

        }
    }
}
