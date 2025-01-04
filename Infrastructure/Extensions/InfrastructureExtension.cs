using Application.Contracts.Infrastructure;
using Domain.Options;
using Infrastructure.Services;
using Infrastructure.Utility;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;

namespace Infrastructure.Extensions
{
    public static class InfrastructureExtension
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenOption>(configuration.GetSection(TokenOption.Key));

            services.AddScoped<ITokenService, TokenService>();

            services.AddAuthorization(options =>
            {
                options.DefaultPolicy = new AuthorizationPolicyBuilder(JwtBearerDefaults.AuthenticationScheme)
                    .RequireAuthenticatedUser()
                    .Build();
            });

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
            {
                var tokenOption = configuration.GetSection(TokenOption.Key).Get<TokenOption>();
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidIssuer = tokenOption!.Issuer,
                    ValidAudience = tokenOption.Audience[0],
                    IssuerSigningKey = SignService.GetSymetricSecurityKey(tokenOption.SecurityKey),
                    ValidateIssuerSigningKey = true,
                    ValidateAudience = true,
                    ValidateIssuer = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });

            services.AddMassTransitServiceExtension(configuration);

            services.AddElasticsearchServiceExtension(configuration);

            services.AddRedisServiceExtension(configuration);

            services.Configure<EmailOption>(configuration.GetSection(EmailOption.Key));
            services.AddScoped<IEmailService, EmailService>();

            return services;
        }
    }
}
