using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Events;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Posts.Delete
{
    public class DeletePostCommandHandler(IPostRepository postRepository, IPostTagRepository postTagRepository, IUnitOfWork unitOfWork, IServiceBus serviceBus) : IRequestHandler<DeletePostCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(DeletePostCommand request, CancellationToken cancellationToken)
        {
            var post = await postRepository.GetByIdAsync(request.Id);

            if (post is null) return ServiceResult.Error("Belirtilen ID'ye sahip post bulunamadı", HttpStatusCode.NotFound);

            // Posta bağlı olan posttagleri sil
            var postTags = await postTagRepository.Where(x => x.PostId == request.Id).ToListAsync();
            if (postTags is not null && postTags.Any())
            {
                foreach (var postTag in postTags)
                {
                    postTagRepository.Delete(postTag);
                }
            }

            postRepository.Delete(post);
            await unitOfWork.SaveChangesAsync();

            await serviceBus.PublishAsync(new PostDeletedEvent(post.Id), cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
