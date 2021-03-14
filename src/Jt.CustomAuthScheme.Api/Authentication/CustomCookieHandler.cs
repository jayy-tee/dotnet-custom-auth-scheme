using System.Net;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;

namespace Jt.CustomAuthScheme.Api.Authentication
{
    public class CustomCookieHandler : IAuthenticationHandler
    {
        private HttpContext _httpContext;
        private readonly CustomSessionManager _sessionManager;

        public CustomCookieHandler(CustomSessionManager sessionManager)
        {
            _sessionManager = sessionManager;
        }

        public Task InitializeAsync(AuthenticationScheme scheme, HttpContext context)
        {
            _httpContext = context;
            return Task.CompletedTask;
        }

        public async Task<AuthenticateResult> AuthenticateAsync()
        {
            var sessionCookie = GetSessionIdCookieName();
            var authCookie = _httpContext.Request.Cookies[sessionCookie];
            
            if (authCookie == null)
            {
                return AuthenticateResult.NoResult();
            }

            return await ValidateCookie(authCookie);
        }
        
        public Task ChallengeAsync(AuthenticationProperties? properties)
        {
            _httpContext.Response.StatusCode = (int) HttpStatusCode.Unauthorized;

            return Task.CompletedTask;
        }

        public Task ForbidAsync(AuthenticationProperties? properties)
        {
            _httpContext.Response.StatusCode = (int) HttpStatusCode.Forbidden;

            return Task.CompletedTask;
        }

        private async Task<AuthenticateResult> ValidateCookie(string theCookie)
        {
            var response = AuthenticateResult.Fail("Default Failure");
            var principal = await _sessionManager.GetPrincipalFromSessionCookieValue(theCookie);

            if (principal.Identity?.IsAuthenticated == true)
            {
                response = AuthenticateResult.Success(new AuthenticationTicket(principal, CustomCookieDefaults.AuthenticationScheme));
            }

            return response;
        }
        
        private string GetSessionIdCookieName()
        {
            var sessionOption = _httpContext.Request.Cookies[CustomCookieDefaults.SessionOptionCookieName] ?? SessionOptions.Temporary;
            var sessionIdCookieName = sessionOption == SessionOptions.Permanent
                ? CustomCookieDefaults.PermanentSessionIdCookieName
                : CustomCookieDefaults.TemporarySessionIdCookieName;
            
            return sessionIdCookieName;
        }
    }
}