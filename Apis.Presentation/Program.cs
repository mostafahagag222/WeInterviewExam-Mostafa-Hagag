using System;
using Apis.Presentation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.RateLimiting;
using System.Threading.RateLimiting;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddCustomConfigurations(builder.Configuration);

builder.Services.AddResponseCompression(options => { options.EnableForHttps = true; });

builder.Services.AddRateLimiter(options =>
{
    options.AddFixedWindowLimiter("Default", limiterOptions =>
    {
        limiterOptions.PermitLimit = 10;
        limiterOptions.QueueLimit = 5;
        limiterOptions.QueueProcessingOrder = QueueProcessingOrder.OldestFirst;
    });
});

var app = builder.Build();

app.UseRateLimiter();

app.UseResponseCompression();

app.MapControllers();

app.Run();