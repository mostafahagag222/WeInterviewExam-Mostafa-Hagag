using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using MVC.Core.Context;
using MVC.Core.Entities;
using MVC.Core.Interfaces.Common;
using MVC.Core.Interfaces.Repositories;
using MVC.Core.Interfaces.Services;
using MVC.Infrastructure.Common;
using MVC.Infrastructure.Repositories;
using MVC.Infrastructure.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<OutagesDbContext>(o =>
    o.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
builder.Services.AddScoped<IJwtService, JwtService>();

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<IUserRepository>(provider.GetRequiredService<IUserRepository>));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<IUserRepository>(provider.GetRequiredService<IUserRepository>));

builder.Services.AddScoped<IChannelRepository, ChannelRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<IChannelRepository>(provider.GetRequiredService<IChannelRepository>));

builder.Services.AddScoped<ICuttingDownDetailRepository, CuttingDownDetailRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<ICuttingDownDetailRepository>(provider.GetRequiredService<ICuttingDownDetailRepository>));

builder.Services.AddScoped<ICuttingDownHeaderRepository, CuttingDownHeaderRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<ICuttingDownHeaderRepository>(provider.GetRequiredService<ICuttingDownHeaderRepository>));

builder.Services.AddScoped<ICuttingDownIgnoredRepository, CuttingDownIgnoredRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<ICuttingDownIgnoredRepository>(provider.GetRequiredService<ICuttingDownIgnoredRepository>));

builder.Services.AddScoped<IFtaProblemTypeRepository, FtaProblemTypeRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<IFtaProblemTypeRepository>(provider.GetRequiredService<IFtaProblemTypeRepository>));

builder.Services.AddScoped<INetworkElementHierarchyPathRepository, NetworkElementHierarchyPathRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<INetworkElementHierarchyPathRepository>(
        provider.GetRequiredService<INetworkElementHierarchyPathRepository>));

builder.Services.AddScoped<INetworkElementRepository, NetworkElementRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<INetworkElementRepository>(provider.GetRequiredService<INetworkElementRepository>));

builder.Services.AddScoped<INetworkElementTypeRepository, NetworkElementTypeRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<INetworkElementTypeRepository>(provider.GetRequiredService<INetworkElementTypeRepository>));

builder.Services.AddScoped<ICuttingDownHeaderRepository, CuttingDownHeaderRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<ICuttingDownHeaderRepository>(provider.GetRequiredService<ICuttingDownHeaderRepository>));

builder.Services.AddScoped<ICuttingDownDetailRepository, CuttingDownDetailRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<ICuttingDownDetailRepository>(provider.GetRequiredService<ICuttingDownDetailRepository>));

builder.Services.AddScoped<IHiearchyPathRepository, HiearchyPathRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<IHiearchyPathRepository>(provider.GetRequiredService<IHiearchyPathRepository>));

builder.Services.AddScoped<INetworkElementRepository, NetworkElementRepository>();
builder.Services.AddScoped(provider =>
    new Lazy<INetworkElementRepository>(provider.GetRequiredService<INetworkElementRepository>));

builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IChannelService, ChannelService>();
builder.Services.AddScoped<IProblemTypeService, ProblemTypeService>();
builder.Services.AddScoped<INetworkElementService, NetworkElementService>();
builder.Services.AddScoped<ICuttingsService, CuttingsService>();
builder.Services.AddScoped<IHiearchyPathRepository, HiearchyPathRepository>();
builder.Services.AddScoped<IHiearchyService, HiearchyService>();

var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]!);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(key)
        };
    });

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

// Set Authorization header from AuthToken cookie
app.Use(async (context, next) =>
{
    var token = context.Request.Cookies["AuthToken"];
    if (!string.IsNullOrEmpty(token))
    {
        context.Request.Headers["Authorization"] = $"Bearer {token}"; // Correctly set header
    }

    await next();
});

app.UseAuthentication();

// Redirect user to Login page if not authenticated, otherwise navigate to home page
app.Use(async (context, next) =>
{
    if (context.Request.Path == "/" && context.Request.Method == "GET")
    {
        if (context.User.Identity is { IsAuthenticated: true })
        {
            context.Response.Redirect("/Cutting/Master");
        }
        else
        {
            context.Response.Redirect("/Auth/Login");
        }

        return;
    }

    await next();
});

app.UseRouting();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Auth}/{action=Login}/{id?}");

app.Run();