using System;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Jt.CustomAuthScheme.Api
{
    public interface IRedisCache
    {
        ConnectionMultiplexer GetConnection();
    }

    public class RedisCache : IRedisCache
    {
        private static Lazy<ConnectionMultiplexer> _lazyRedisConn;

        public RedisCache(IConfiguration configuration)
        {
            _lazyRedisConn = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect("localhost:16379"));
        }

        public ConnectionMultiplexer GetConnection() => _lazyRedisConn.Value;
    }
}