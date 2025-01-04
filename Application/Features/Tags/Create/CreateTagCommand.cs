using MediatR;

namespace Application.Features.Tags.Create
{
    public record CreateTagCommand(string Name) : IRequest<ServiceResult<CreateTagResponse>>;
}
