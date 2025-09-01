using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using RentalPlatform.Infrastructure.Services;
using Microsoft.Extensions.DependencyInjection;
using RentalPlatform.Infrastructure.Persistence;
using RentalPlatform.Application.Common.Interfaces;
using RentalPlatform.Infrastructure.Persistence.Mongo;
using RentalPlatform.Infrastructure.Persistence.Repositories;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace RentalPlatform.Infrastructure;
           
public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        services.AddDbContext<RentalDbContext>(options =>
            options.UseNpgsql(connectionString));

        var mongoDbSettings = configuration.GetSection("MongoDb");
        services.Configure<MongoDbSettings>(mongoDbSettings);

        services.AddSingleton<IMongoClient>(sp =>
        {
            var settings = sp.GetRequiredService<IOptions<MongoDbSettings>>().Value;
            return new MongoClient(settings.ConnectionString);
        });

        services.AddScoped<IMotorcycleRepository, MotorcycleRepository>();
        services.AddScoped<IRentalRepository, RentalRepository>();
        services.AddScoped<INotificationRepository, MongoNotificationRepository>();
        services.AddScoped<IDriverRepository, DriverRepository>();
        services.AddScoped<IMessagePublisher, MassTransitMessagePublisher>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        services.AddSingleton<IFileStorageService, MinioFileStorageService>();

        return services;
    }
}