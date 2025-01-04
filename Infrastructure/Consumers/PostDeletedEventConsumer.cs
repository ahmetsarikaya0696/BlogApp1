using Application.Contracts.Infrastructure;
using Application.Events;
using MassTransit;

namespace Infrastructure.Consumers
{
    public class PostDeletedEventConsumer(IPostElasticsearchRepository postElasticsearchRepository) : IConsumer<PostDeletedEvent>
    {
        public async Task Consume(ConsumeContext<PostDeletedEvent> context)
        {
            await postElasticsearchRepository.DeleteAsync(context.Message.Id);
        }
    }
}
