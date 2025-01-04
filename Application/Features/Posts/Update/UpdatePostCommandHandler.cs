using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Events;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Posts.Update
{
    public class UpdatePostCommandHandler(IPostRepository postRepository, IPostTagRepository postTagRepository, IUnitOfWork unitOfWork, IMapper mapper, IServiceBus serviceBus) : IRequestHandler<UpdatePostCommand, ServiceResult>
    {
        public async Task<ServiceResult> Handle(UpdatePostCommand request, CancellationToken cancellationToken)
        {
            var post = await postRepository.GetByIdAsync(request.Id);

            if (post is null) return ServiceResult.Error("Belirtilen ID'ye sahip bir post bulunamadı", HttpStatusCode.NotFound);

            mapper.Map(request, post);

            // TODO : Bu kısım posttag repository içine alınabilir??
            #region Tag'leri Posta Ekleme
            // İlişkili kayıtları sil
            var postTags = await postTagRepository.Where(x => x.PostId == request.Id).ToListAsync();
            foreach (var postTag in postTags)
            {
                postTagRepository.Delete(postTag);
            }

            await unitOfWork.SaveChangesAsync();

            // Yeniden ilişkilendir
            foreach (var tagId in request.tagIds)
            {
                await postTagRepository.AddAsync(new PostTag() { PostId = request.Id, TagId = tagId });
            }
            //await unitOfWork.SaveChangesAsync(); 
            #endregion

            postRepository.Update(post);
            await unitOfWork.SaveChangesAsync();

            await serviceBus.PublishAsync(new PostUpdatedEvent(post.Id, post.AuthorId, request.tagIds), cancellationToken);

            return ServiceResult.SuccessAsNoContent();
        }
    }
}
