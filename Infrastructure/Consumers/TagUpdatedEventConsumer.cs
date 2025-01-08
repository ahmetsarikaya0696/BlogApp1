using Application.Contracts.Infrastructure;
using Application.Dtos;
using Application.Events;
using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Consumers
{
    public class TagUpdatedEventConsumer(IPostElasticsearchRepository postElasticsearchRepository) : IConsumer<TagUpdatedEvent>
    {
        public async Task Consume(ConsumeContext<TagUpdatedEvent> context)
        {
            var tagId = context.Message.TagId;
            var tagName = context.Message.TagName;

            var postSearchDto = new PostSearchDto(string.Empty, string.Empty, null, null, string.Empty, string.Empty, string.Empty);
            var allPosts = await postElasticsearchRepository.SearchAsync(postSearchDto, 1, 10000);

            var postsToUpdate = allPosts.list.Where(p => p.Tags.Any(t => t.TagId == tagId)).ToList();
            var newPosts = postsToUpdate.Select(p =>
            {
                var updatedTags = p.Tags.Select(t => t.TagId == tagId ? new TagDto(tagId, tagName) : t).ToList();
                return new PostDto(p.Id, p.Title, p.Content, p.ViewCount, p.CreatedDate, p.LastModifiedDate, p.Author, updatedTags);
            }).ToList();

            foreach (var post in newPosts)
            {
                await postElasticsearchRepository.UpdateAsync(post);
            }
        }
    }
}
