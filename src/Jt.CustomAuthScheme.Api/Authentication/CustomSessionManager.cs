using System.Security.Claims;
using System.Text.Json;
using Microsoft.Extensions.Logging;

namespace Jt.CustomAuthScheme.Api.Authentication
{
    public class CustomSessionManager
    {
        private const string SessionPrefix = "urn:iauthsession:";
        private static readonly JsonSerializerOptions JsonSerializerOptions = new JsonSerializerOptions()
        {
            PropertyNameCaseInsensitive = true
        };
        
        private readonly IRedisCache _redisCache;
        private readonly ILogger<CustomSessionManager> _logger;
        private readonly IServiceStackSessionMapper _serviceStackSessionMapper;

        public CustomSessionManager(IRedisCache redisCache, ILogger<CustomSessionManager> logger, IServiceStackSessionMapper serviceStackSessionMapper)
        {
            _logger = logger;
            _redisCache = redisCache;
            _serviceStackSessionMapper = serviceStackSessionMapper;
        }

        public ClaimsPrincipal GetPrincipalFromSessionCookieValue(string cookieValue)
        {
            _logger.LogDebug("Accessing session in redis");
            var db = _redisCache.GetConnection().GetDatabase();
            var session = db.StringGet($"{SessionPrefix}{cookieValue}");
            
            if (!session.HasValue)
            {
                _logger.LogWarning($"Authentication failed. No session found with ID {cookieValue}");
                return new ClaimsPrincipal();
            }

            var authSession = JsonSerializer.Deserialize<ServiceStackSession>(session.ToString(), JsonSerializerOptions);
            
            return _serviceStackSessionMapper.CreatePrincipalFromSession(authSession);
        }
    }
}