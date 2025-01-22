using System;
using Apis.Core.Context;
using Apis.Core.Interfaces.Common;
using Apis.Core.Interfaces.Repositories;
using Apis.Core.Interfaces.Services;
using Apis.Infrastructure.Common;
using Apis.Infrastructure.Repositories;
using Apis.Infrastructure.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Apis.Presentation;

public static class Configurations
{
    public static IServiceCollection AddCustomConfigurations(this IServiceCollection services,
        IConfiguration configuration)
    {
        services.AddDbContext<OutagesDbContext>(o =>
            o.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();


        services.AddScoped<ICabinRepository, CabineRepository>();
        services.AddScoped<Lazy<ICabinRepository>>(provider =>
            new Lazy<ICabinRepository>(provider.GetRequiredService<ICabinRepository>));

        services.AddScoped<ICableRepository, CableRepository>();
        services.AddScoped<Lazy<ICableRepository>>(provider =>
            new Lazy<ICableRepository>(provider.GetRequiredService<ICableRepository>));
        
        services.AddScoped<ICuttingDownARepository, CuttingDownARepository>();
        services.AddScoped<Lazy<ICuttingDownARepository>>(provider =>
            new Lazy<ICuttingDownARepository>(provider.GetRequiredService<ICuttingDownARepository>));
        
        services.AddScoped<ICuttingDownBRepository, CuttingDownBRepository>();
        services.AddScoped<Lazy<ICuttingDownBRepository>>(provider =>
            new Lazy<ICuttingDownBRepository>(provider.GetRequiredService<ICuttingDownBRepository>));
        
        services.AddScoped<IStaProblemTypeRepository, StaProblemTypeRepository>();
        services.AddScoped<Lazy<IStaProblemTypeRepository>>(provider =>
            new Lazy<IStaProblemTypeRepository>(provider.GetRequiredService<IStaProblemTypeRepository>));

        services.AddScoped<ICuttingAService, CuttingAService>();
        services.AddScoped<ICuttingBService, CuttingBService>();


        return services;
    }
}