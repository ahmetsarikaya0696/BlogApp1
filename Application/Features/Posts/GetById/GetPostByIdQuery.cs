using MediatR;

namespace Application.Features.Posts.GetById
{
    public record GetPostByIdQuery(Guid Id) : IRequest<ServiceResult<GetPostByIdResponse>>;
}
