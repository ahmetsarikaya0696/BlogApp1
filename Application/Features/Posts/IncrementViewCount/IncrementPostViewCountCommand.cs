using MediatR;

namespace Application.Features.Posts.IncrementViewCount
{
    public record IncrementPostViewCountCommand(Guid Id) : IRequest<ServiceResult>;
}
