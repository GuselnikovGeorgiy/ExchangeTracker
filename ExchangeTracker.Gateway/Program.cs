using BinanceExchange;
using BybitExchange;
using Exchanges.Abstractions.Enums;
using ExchangeTracker.Core;
using ExchangeTracker.Gateway.Configurations;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureOptions(builder.Configuration);
builder.Services.AddControllers();
builder.Services.ConfigureSwaggerServices();
builder.Services.ConfigureMapper();

builder.Services.ConfigureCoreServices();
builder.Services.ConfigureCoreBinanceServices(nameof(ExchangeClientTypeEnum.Binance));
builder.Services.ConfigureCoreBybitServices(nameof(ExchangeClientTypeEnum.Bybit));

var app = builder.Build();

app.ConfigureSwaggerPipeline();
app.UseHttpsRedirection();
app.MapControllers();

app.Run();
