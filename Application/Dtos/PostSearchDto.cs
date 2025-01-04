namespace Application.Dtos
{
    public record PostSearchDto(string Title, string Content, DateTime? CreatedDate, DateTime? LastModifiedDate, string AuthorFullName, string AuthorUserName, string TagName);
}
