using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Dtos;
using Application.Events;
using Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Consumers
{
    public class PostUpdatedEventConsumer(IPostRepository postRepository, UserManager<User> userManager, ITagRepository tagRepository, IPostElasticsearchRepository postElasticsearchRepository) : IConsumer<PostUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<PostUpdatedEvent> context)
        {
            var postId = context.Message.PostId;
            var userId = context.Message.UserId;
            var tagIds = context.Message.TagIds;
            var post = await postRepository.GetByIdAsync(postId);

            var user = await userManager.FindByIdAsync(userId);
            var author = new AuthorDto(user!.Id, $"{user.FirstName} {user.LastName}", user.UserName!);

            var tags = new List<TagDto>();
            foreach (var tagId in tagIds)
            {
                var tag = await tagRepository.GetByIdAsync(tagId);
                tags.Add(new TagDto(tagId, tag!.Name));
            }

            var createOrUpdatePostDto = new PostDto(post!.Id, post.Title, post.Content, post.ViewCount, post.CreatedDate, post.LastModifiedDate, author, tags);
            await postElasticsearchRepository.UpdateAsync(createOrUpdatePostDto);
        }
    }
}
