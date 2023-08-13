using Innoloft.Events.Api.Models;
using Innoloft.Events.Api.Services.Interfaces;
using Innoloft.Events.Api.Services.Providers;
using StackExchange.Redis;

namespace Innoloft.Events.Api.Extensions;

public static class ServiceCollectionExtension
{

    public static IServiceCollection AddRedisStorage(this IServiceCollection services,
        RedisConfiguration configureOptions)
    {
        if (services == null)
        {
            throw new ArgumentNullException(nameof(services));
        }

        services.Configure<RedisConfiguration>(c =>
        {
            c.Port = configureOptions.Port;
            c.Database = configureOptions.Database;
            c.Server = configureOptions.Server;
        });
        var configuration = ConfigurationOptions.Parse($"{configureOptions.Server}:{configureOptions.Port}", true);
        configuration.ResolveDns = false;
        configuration.AbortOnConnectFail = false;
        configuration.AllowAdmin = true;
        configuration.DefaultDatabase = configureOptions.Database;
        configuration.ReconnectRetryPolicy = new LinearRetry(500);


        var connection = ConnectionMultiplexer.Connect(configuration);
        var db = connection.GetDatabase();
        var endPoint = $"{configureOptions.Server}:{configureOptions.Port}";
        var redisServer = connection.GetServer(endPoint);
        services.AddSingleton<IRedisCacheManager>((sp) =>
            new RedisCacheManager(db, redisServer));

        return services;
    }
}