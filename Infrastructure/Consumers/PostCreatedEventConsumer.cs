using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Dtos;
using Application.Events;
using Domain.Entities;
using MassTransit;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Consumers
{
    public class PostCreatedEventConsumer(IPostRepository postRepository, UserManager<User> userManager, ITagRepository tagRepository, IPostElasticsearchRepository postElasticsearchRepository) : BasePostEventConsumer(userManager, tagRepository), IConsumer<PostCreatedEvent>
    {
        public async Task Consume(ConsumeContext<PostCreatedEvent> context)
        {
            var postId = context.Message.PostId;
            var userId = context.Message.UserId;
            var tagIds = context.Message.TagIds;

            var post = await postRepository.GetByIdAsync(postId);

            AuthorDto author = await GetAuthor(userId);

            List<TagDto> tags = await GetTags(tagIds);

            var postDto = new PostDto(post!.Id, post.Title, post.Content, post.ViewCount, post.CreatedDate, post.LastModifiedDate, author, tags);

            await postElasticsearchRepository.SavePostAsync(postDto);
        }
    }
}
