namespace Domain.Options
{
    public class TokenOption
    {
        public const string Key = "TokenOption";
        public List<string> Audience { get; set; } = [];
        public required string Issuer { get; set; }
        public int AccessTokenExpiration { get; set; }
        public int RefreshTokenExpiration { get; set; }
        public required string SecurityKey { get; set; }
    }
}
