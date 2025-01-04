using Application.Contracts.Infrastructure;
using Domain.Options;
using Elastic.Clients.Elasticsearch;
using Elastic.Transport;
using Infrastructure.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class Elasticsearch
    {
        public static IServiceCollection AddElasticsearchServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            var elasticsearchOption = configuration.GetSection(ElasticsearchOption.Key).Get<ElasticsearchOption>();

            string username = elasticsearchOption!.Username;
            string password = elasticsearchOption!.Password;

            var settings = new ElasticsearchClientSettings(new Uri(elasticsearchOption.Url!));
            settings.Authentication(new BasicAuthentication(username, password));

            var client = new ElasticsearchClient(settings);

            services.AddSingleton(client);

            services.AddScoped<IPostElasticsearchRepository, PostElasticsearchRepository>();

            return services;
        }
    }
}
