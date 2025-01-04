using Domain.Configuration;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Application.Extensions
{
    public static class ApplicationExtension
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddFluentValidationAutoValidation();
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            services.AddMediatR(x => x.RegisterServicesFromAssemblyContaining(typeof(ApplicationAssembly)));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            services.Configure<List<Client>>(configuration.GetSection(Client.Key));

            return services;
        }
    }
}