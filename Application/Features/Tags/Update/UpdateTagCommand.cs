using MediatR;

namespace Application.Features.Tags.Update
{
    public record UpdateTagCommand(Guid Id, string Name) : IRequest<ServiceResult>;
}
