using StackExchange.Redis;

namespace Infrastructure.Services
{
    public class RedisRepository(string url)
    {
        private readonly ConnectionMultiplexer _connectionMultiplexer = ConnectionMultiplexer.Connect(url);

        public IDatabase GetDatabase(int dbIndex = 0)
        {
            return _connectionMultiplexer.GetDatabase(dbIndex);
        }
    }
}
