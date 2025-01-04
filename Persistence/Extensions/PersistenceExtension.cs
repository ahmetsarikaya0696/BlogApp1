using Application.Contracts.Persistence;
using Domain.Entities;
using Domain.Options;
using Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Interceptors;
using Persistence.PostLikes;
using Persistence.Posts;
using Persistence.PostTags;
using Persistence.RefreshTokens;
using Persistence.Tags;

namespace Persistence.Extensions
{
    public static class PersistenceExtension
    {
        public static IServiceCollection AddPesistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<AppDbContext>(options =>
            {
                var connectionStringOption = configuration.GetSection(ConnectionStringOption.Key).Get<ConnectionStringOption>();
                options.UseSqlServer(connectionStringOption!.SqlServer, sqlServerOptionsAction =>
                {
                    sqlServerOptionsAction.MigrationsAssembly(typeof(PersistenceAssembly).Assembly.FullName);
                });

                options.AddInterceptors(new AuditDbContextInterceptor());
            });

            services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
            services.AddScoped<ITagRepository, TagRepository>();
            services.AddScoped<IRefreshTokenRepository, RefreshTokenRepository>();
            services.AddScoped<IPostTagRepository, PostTagRepository>();
            services.AddScoped<IPostLikeRepository, PostLikeRepository>();

            // Decorator pattern
            services.AddScoped<IPostRepository>(serviceProvider =>
            {
                var appDbContext = serviceProvider.GetRequiredService<AppDbContext>();
                var postRepository = new PostRepository(appDbContext);
                var redisRepository = serviceProvider.GetRequiredService<RedisRepository>();

                return new CachedPostRepositoryDecorator(postRepository, redisRepository);
            });

            services.AddScoped<IUnitOfWork, UnitOfWork>();

            services.AddIdentity<User, IdentityRole>(options =>
            {
                options.User.RequireUniqueEmail = true;
                options.Password.RequireNonAlphanumeric = false;
                options.Password.RequireDigit = true;
            }).AddEntityFrameworkStores<AppDbContext>()
              .AddDefaultTokenProviders();

            return services;
        }
    }
}
