using Application.Contracts.Infrastructure;
using Application.Events;
using MediatR;

namespace Application.Features.Posts.IncrementViewCount
{
    public class IncrementPostViewCountCommandHandler(IServiceBus serviceBus) : IRequestHandler<IncrementPostViewCountCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(IncrementPostViewCountCommand request, CancellationToken cancellationToken)
        {
            await serviceBus.PublishAsync(new PostViewCountIncrementedEvent(request.Id), cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
