using Domain.Options;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class Redis
    {
        public static IServiceCollection AddRedisServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<RedisRepository>(serviceProvider =>
            {
                return new RedisRepository(configuration.GetSection(CacheOption.Key).Get<CacheOption>()!.Url);
            });

            return services;
        }
    }
}