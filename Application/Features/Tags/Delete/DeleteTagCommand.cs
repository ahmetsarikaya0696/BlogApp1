using MediatR;

namespace Application.Features.Tags.Delete
{
    public record DeleteTagCommand(Guid Id) : IRequest<ServiceResult>;
}
