using Microsoft.AspNetCore.Builder;
using JOX.Assistant.Socket;

var builder = WebApplication.CreateBuilder(args);
var startup = new Api.Startup(builder.Configuration);

startup.ConfigureServices(builder.Services);
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
startup.Configure(app, app.Environment);
app.UseHttpsRedirection();
app.MapControllers();
app.Run();
