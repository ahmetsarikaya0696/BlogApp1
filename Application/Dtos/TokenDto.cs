namespace Application.Dtos
{
    public record TokenDto(string AccessToken, DateTime AccessTokenExpiration, string RefreshToken, DateTime RefreshTokenExpiration);
}
