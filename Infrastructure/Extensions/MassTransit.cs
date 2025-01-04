using Application.Contracts.Infrastructure;
using Domain.Constants;
using Domain.Options;
using Infrastructure.Consumers;
using MassTransit;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Infrastructure.Extensions
{
    public static class MassTransit
    {
        public static IServiceCollection AddMassTransitServiceExtension(this IServiceCollection services, IConfiguration configuration)
        {
            // MassTransit
            services.AddScoped<IServiceBus, ServiceBus>();

            var serviceBusOption = configuration.GetSection(ServiceBusOption.Key).Get<ServiceBusOption>();

            services.AddMassTransit(x =>
            {
                x.AddConsumer<PostCreatedEventConsumer>();
                x.AddConsumer<PostUpdatedEventConsumer>();
                x.AddConsumer<PostDeletedEventConsumer>();
                x.AddConsumer<PostViewCountIncrementedEventConsumer>();
                x.AddConsumer<PostLikeChangedEventConsumer>();
                x.AddConsumer<UserCreatedEventConsumer>();

                x.UsingRabbitMq((context, cfg) =>
                {
                    cfg.Host(new Uri(serviceBusOption!.Url), h =>
                    {
                    });

                    cfg.ReceiveEndpoint(ServiceBusConstants.PostCreatedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<PostCreatedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(ServiceBusConstants.PostUpdatedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<PostUpdatedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(ServiceBusConstants.PostDeletedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<PostDeletedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(ServiceBusConstants.PostViewCountIncrementedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<PostViewCountIncrementedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(ServiceBusConstants.PostLikeChangedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<PostLikeChangedEventConsumer>(context);
                    });

                    cfg.ReceiveEndpoint(ServiceBusConstants.UserCreatedEventQueueName, e =>
                    {
                        e.ConfigureConsumer<UserCreatedEventConsumer>(context);
                    });
                });
            });

            return services;
        }
    }
}
