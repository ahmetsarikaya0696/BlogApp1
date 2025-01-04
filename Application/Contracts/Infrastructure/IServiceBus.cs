using Application.Events;

namespace Application.Contracts.Infrastructure
{
    public interface IServiceBus
    {
        Task PublishAsync<T>(T message, CancellationToken cancellationToken) where T : IEventOrMessage;
        Task SendAsync<T>(T message, string queueName, CancellationToken cancellationToken) where T : IEventOrMessage;

    }
}
