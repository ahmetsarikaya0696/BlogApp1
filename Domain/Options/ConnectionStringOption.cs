namespace Domain.Options
{
    public class ConnectionStringOption
    {
        public const string Key = "ConnectionStrings";
        public required string SqlServer { get; set; }
    }
}
