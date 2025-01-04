using MediatR;

namespace Application.Features.Posts.Create
{
    public record CreatePostCommand(string Title, string Content, List<Guid> TagIds, string AuthorId) : IRequest<ServiceResult<CreatePostResponse>>;
}
