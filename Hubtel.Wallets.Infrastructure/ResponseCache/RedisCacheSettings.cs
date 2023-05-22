namespace Hubtel.Wallets.Infrastructure.ResponseCache
{
    public class RedisCacheSettings
    {
        public bool Enabled { get; set; }
        public string RedisConnectionString { get; set; }
    }
}