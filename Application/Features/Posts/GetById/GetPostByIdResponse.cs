namespace Application.Features.Posts.GetById
{
    public record GetPostByIdResponse(Guid Id, string Title, string Content, string AuthorUserName, int ViewCount, int LikeCount, List<string> TagNames);
}
