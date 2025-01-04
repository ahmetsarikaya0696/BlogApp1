using Application.Contracts.Infrastructure;
using Application.Events;
using MediatR;

namespace Application.Features.Posts.Like
{
    internal class CreateOrDeletePostLikeCommandHandler(IServiceBus serviceBus) : IRequestHandler<CreateOrDeletePostLikeCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(CreateOrDeletePostLikeCommand request, CancellationToken cancellationToken)
        {
            await serviceBus.PublishAsync(new PostLikeChangedEvent(request.PostId, request.UserId, request.IsLiked), cancellationToken);
            return ServiceResult.SuccessAsNoContent();
        }
    }
}
