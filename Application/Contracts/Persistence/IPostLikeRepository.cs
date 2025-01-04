using Domain.Entities;

namespace Application.Contracts.Persistence
{
    public interface IPostLikeRepository : IGenericRepository<PostLike>
    {
        Task<PostLike?> GetByPostIdAndUserIdAsync(Guid postId, string userId);
    }
}
