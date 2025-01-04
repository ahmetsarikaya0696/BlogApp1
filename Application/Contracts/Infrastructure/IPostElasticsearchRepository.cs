using Application.Dtos;

namespace Application.Contracts.Infrastructure
{
    public interface IPostElasticsearchRepository
    {
        Task SavePostAsync(PostDto postDto);
        Task UpdateAsync(PostDto postDto);
        Task DeleteAsync(Guid id);
        Task<(List<PostDto> list, long count)> SearchAsync(PostSearchDto postSearchDto, int page, int pageSize);
    }
}
