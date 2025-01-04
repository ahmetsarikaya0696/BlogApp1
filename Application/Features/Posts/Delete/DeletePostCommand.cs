using MediatR;

namespace Application.Features.Posts.Delete
{
    public record DeletePostCommand(Guid Id) : IRequest<ServiceResult>;
}
