using Application.Contracts.Persistence;
using Application.Events;
using Domain.Entities;
using Infrastructure.Services;
using MassTransit;
using StackExchange.Redis;
using System.Text.Json;

namespace Infrastructure.Consumers
{
    public class PostLikeChangedEventConsumer(IPostLikeRepository postLikeRepository, RedisRepository redisRepository, IUnitOfWork unitOfWork) : IConsumer<PostLikeChangedEvent>
    {
        private readonly IDatabase cacheRepository = redisRepository.GetDatabase();
        private const string postLikesKey = "post_likes";
        private const int likeThreshold = 2;

        public async Task Consume(ConsumeContext<PostLikeChangedEvent> context)
        {
            var postLike = new PostLike { PostId = context.Message.PostId, UserId = context.Message.UserId };

            if (context.Message.IsLiked)
            {
                var jsonPostLike = JsonSerializer.Serialize(postLike);
                await cacheRepository.HashSetAsync(postLikesKey, $"{context.Message.PostId}_{context.Message.UserId}", jsonPostLike);
            }
            else
            {
                var existingPostLike = await postLikeRepository.GetByPostIdAndUserIdAsync(context.Message.PostId, context.Message.UserId);
                if (existingPostLike is not null)
                {
                    await cacheRepository.HashDeleteAsync(postLikesKey, $"{context.Message.PostId}_{context.Message.UserId}");
                }
            }

            await SyncPostLikesWithDatabase();
        }

        private async Task SyncPostLikesWithDatabase()
        {
            var likeCount = await cacheRepository.HashLengthAsync(postLikesKey);

            if (likeCount >= likeThreshold)
            {
                var allLikes = await cacheRepository.HashGetAllAsync(postLikesKey);

                if (allLikes is not null && allLikes.Any())
                {
                    foreach (var like in allLikes)
                    {
                        var postLikeToAdd = JsonSerializer.Deserialize<PostLike>(like.Value!);
                        if (postLikeToAdd is not null) await postLikeRepository.AddAsync(postLikeToAdd);
                    }
                    await unitOfWork.SaveChangesAsync();

                    // Clear the cache after persisting to the database
                    await cacheRepository.KeyDeleteAsync(postLikesKey);
                }
            }
        }
    }
}
