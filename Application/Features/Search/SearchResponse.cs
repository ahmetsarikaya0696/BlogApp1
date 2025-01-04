using Application.Dtos;

namespace Application.Features.Search
{
    public record SearchResponse(List<PostDto> Posts, long TotalCount, long PageLinkCount);
}
