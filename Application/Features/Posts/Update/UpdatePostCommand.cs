using MediatR;

namespace Application.Features.Posts.Update
{
    public record UpdatePostCommand(Guid Id, string Title, string Content, List<Guid> tagIds) : IRequest<ServiceResult>;
}
