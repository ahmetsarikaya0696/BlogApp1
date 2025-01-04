namespace Application.Dtos
{
    public record PostDto(Guid Id, string Title, string Content, int ViewCount, DateTime CreatedDate, DateTime? LastModifiedDate, AuthorDto Author, List<TagDto> Tags);
}
