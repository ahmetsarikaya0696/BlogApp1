using MediatR;

namespace Application.Features.Posts.Like
{
    public record CreateOrDeletePostLikeCommand(Guid PostId, string UserId, bool IsLiked) : IRequest<ServiceResult>;
}
