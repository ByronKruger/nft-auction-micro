using NftApi.Data;
using NftApi.Interfaces;
using Microsoft.EntityFrameworkCore;
// using Nethereum.ABI.EIP712;
using NftApi.Middleware;
using NftApi.Helpers;
using NftApi.Services;
using NftApi.SignalR;
using Microsoft.AspNetCore.SignalR;


namespace NftApi.Extensions;

public static class ApplicationExtensions
{
    public static IServiceCollection AddApplicationServices(this IServiceCollection services, 
        IConfiguration config)
    {
        services.AddScoped<ErrorHandleMiddleware>();
        services.AddScoped<ITokenService, TokenService>();
        // services.AddScoped<IUserRepository, UserRepository>();
        // services.AddScoped<INftRepository, NftRepository>();
        // services.AddScoped<IAuctionRepository, AuctionRepository>();
        services.AddScoped<INftService, NftService>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IAuctionService, AuctionService>();
        services.AddScoped<IImageService, ImageService>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();
        services.AddDbContext<DataContext>(opts => 
        {
            opts.UseSqlite(config.GetConnectionString("DefaultConnection"));
        });
        services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

        services.AddSingleton<IHubFilter, ExceptionFilter>();

        services.AddSignalR(options => 
        { 
            // options.AddFilter<ExceptionFilter>();
            // options.AddFilter(new ExceptionFilter());
            options.EnableDetailedErrors = true;
        });

        // services.AddControllers().AddJsonOptions(options =>
        // {
        //     options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.Preserve;
        // });

        services.Configure<CloudinarySettings>(config.GetSection("CloudinarySettings"));
        
        return services;
    }

}
