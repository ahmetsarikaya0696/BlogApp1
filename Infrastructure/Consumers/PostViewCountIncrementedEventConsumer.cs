using Application.Contracts.Persistence;
using Application.Events;
using MassTransit;

namespace Infrastructure.Consumers
{
    public class PostViewCountIncrementedEventConsumer(IPostRepository postRepository, IUnitOfWork unitOfWork) : IConsumer<PostViewCountIncrementedEvent>
    {
        public async Task Consume(ConsumeContext<PostViewCountIncrementedEvent> context)
        {
            var post = await postRepository.GetByIdAsync(context.Message.Id);

            if (post is null) throw new Exception($"{context.Message.Id} ID'li post bulunamadı!");

            post.ViewCount++;

            postRepository.Update(post);
            await unitOfWork.SaveChangesAsync();
        }
    }
}
