namespace Domain.Options
{
    public class ElasticsearchOption
    {
        public const string Key = "Elasticsearch";
        public required string Url { get; set; }
        public required string Username { get; set; }
        public required string Password { get; set; }
    }
}
