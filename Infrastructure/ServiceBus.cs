using Application.Contracts.Infrastructure;
using Application.Events;
using MassTransit;

namespace Infrastructure
{
    public class ServiceBus(IPublishEndpoint publishEndpoint, ISendEndpointProvider sendEndpointProvider) : IServiceBus
    {
        public async Task PublishAsync<T>(T @event, CancellationToken cancellationToken) where T : IEventOrMessage
        {
            await publishEndpoint.Publish(@event, cancellationToken);
        }

        public async Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T : IEventOrMessage
        {
            var endpoint = await sendEndpointProvider.GetSendEndpoint(new Uri($"queue:{queueName}"));

            await endpoint.Send(message, cancellationToken);
        }
    }
}
