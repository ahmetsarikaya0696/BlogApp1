namespace Domain.Options
{
    public class EmailOption
    {
        public const string Key = "EmailOption";
        public required string Host { get; set; }
        public int Port { get; set; }
        public required string User { get; set; }
        public required string Password { get; set; }
    }
}
