using ExchangeTracker.Gateway.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.ConfigureSwaggerServices();
builder.Services.ConfigureMapper();

var app = builder.Build();

app.ConfigureSwaggerPipeline();
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
