using Application.Contracts.Infrastructure;
using Application.Contracts.Persistence;
using Application.Events;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Features.Posts.Create
{
    public class CreatePostCommandHandler(IPostRepository postRepository, IPostTagRepository postTagRepository, IUnitOfWork unitOfWork, IMapper mapper, IServiceBus serviceBus) : IRequestHandler<CreatePostCommand, ServiceResult<CreatePostResponse>>
    {
        public async Task<ServiceResult<CreatePostResponse>> Handle(CreatePostCommand request, CancellationToken cancellationToken)
        {
            var mappedPost = mapper.Map<Post>(request);
            await postRepository.AddAsync(mappedPost);

            foreach (var tagId in request.TagIds)
            {
                await postTagRepository.AddAsync(new PostTag() { TagId = tagId, PostId = mappedPost.Id });
            }

            await unitOfWork.SaveChangesAsync();

            await serviceBus.PublishAsync(new PostCreatedEvent(mappedPost.Id, mappedPost.AuthorId, request.TagIds), cancellationToken);

            return ServiceResult<CreatePostResponse>.SuccessAsCreated(new CreatePostResponse(mappedPost.Id), $"api/posts/{mappedPost.Id}");
        }
    }
}
