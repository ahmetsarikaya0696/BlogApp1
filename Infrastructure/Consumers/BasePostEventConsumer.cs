using Application.Contracts.Persistence;
using Application.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Consumers
{
    public abstract class BasePostEventConsumer
    {
        private readonly UserManager<User> _userManager;
        private readonly ITagRepository _tagRepository;

        protected BasePostEventConsumer(UserManager<User> userManager, ITagRepository tagRepository)
        {
            _userManager = userManager;
            _tagRepository = tagRepository;
        }

        protected async Task<AuthorDto> GetAuthor(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            var author = new AuthorDto(user!.Id, $"{user.FirstName} {user.LastName}", user.UserName!);
            return author;
        }

        protected async Task<List<TagDto>> GetTags(List<Guid> tagIds)
        {
            var tags = new List<TagDto>();
            foreach (var tagId in tagIds)
            {
                var tag = await _tagRepository.GetByIdAsync(tagId);
                tags.Add(new TagDto(tagId, tag!.Name));
            }

            return tags;
        }
    }
}
