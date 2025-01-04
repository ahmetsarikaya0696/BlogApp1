namespace Domain.Configuration
{
    public class Client
    {
        public const string Key = "Clients";
        public required string Id { get; set; }
        public required string Secret { get; set; }
        public List<string> Audiences { get; set; } = [];
    }
}
