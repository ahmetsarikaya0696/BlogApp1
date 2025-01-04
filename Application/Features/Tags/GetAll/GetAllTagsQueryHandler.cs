using Application.Contracts.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Features.Tags.GetAll
{
    public class GetAllTagsQueryHandler(ITagRepository tagRepository) : IRequestHandler<GetAllTagsQuery, ServiceResult<List<TagDto>>>
    {
        public async Task<ServiceResult<List<TagDto>>> Handle(GetAllTagsQuery request, CancellationToken cancellationToken)
        {
            var tags = await tagRepository.GetAll().ToListAsync();

            var tagsAsDto = tags.Select(x => new TagDto(x.Id, x.Name)).ToList();

            return ServiceResult<List<TagDto>>.SuccessAsOk(tagsAsDto);
        }
    }
}
