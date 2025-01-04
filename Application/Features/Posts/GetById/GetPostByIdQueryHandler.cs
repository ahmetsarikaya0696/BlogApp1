using Application.Contracts.Persistence;
using Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Application.Features.Posts.GetById
{
    public class GetPostByIdQueryHandler(IPostRepository postRepository, IPostTagRepository postTagRepository, ITagRepository tagRepository, IPostLikeRepository postLikeRepository, UserManager<User> userManager) : IRequestHandler<GetPostByIdQuery, ServiceResult<GetPostByIdResponse>>
    {
        public async Task<ServiceResult<GetPostByIdResponse>> Handle(GetPostByIdQuery request, CancellationToken cancellationToken)
        {
            var post = await postRepository.GetByIdAsync(request.Id);

            if (post is null) return ServiceResult<GetPostByIdResponse>.Error("Belirtilen ID'ye sahip post bulunamadı", HttpStatusCode.NotFound);

            var postTags = await postTagRepository.Where(x => x.PostId == request.Id).ToListAsync();

            var tagIds = postTags.Select(x => x.TagId).ToList();

            List<string> tagNames = [];
            foreach (var tagId in tagIds)
            {
                var tag = await tagRepository.GetByIdAsync(tagId);

                if (tag is null) continue;

                tagNames.Add(tag.Name);
            }

            var author = await userManager.FindByIdAsync(post.AuthorId);

            if (author is null) return ServiceResult<GetPostByIdResponse>.Error("Belirtilen posta ait yazar bulunamadı", HttpStatusCode.NotFound);

            var likeCount = await postLikeRepository.CountAsync(x => x.PostId == request.Id);

            return ServiceResult<GetPostByIdResponse>.SuccessAsOk(new GetPostByIdResponse(post.Id, post.Title, post.Content, author.UserName!, post.ViewCount, likeCount, tagNames));
        }
    }
}
