using System.Security.Claims;

namespace Jt.CustomAuthScheme.Api.Authentication
{
    public interface IServiceStackSessionMapper
    {
        ClaimsPrincipal CreatePrincipalFromSession(ServiceStackSession authSession);
    }

    public class ServiceStackSessionMapper : IServiceStackSessionMapper
    {
        public ClaimsPrincipal CreatePrincipalFromSession(ServiceStackSession authSession)
        {
            var identity = new ClaimsIdentity(CustomCookieDefaults.AuthenticationScheme);
            identity.AddClaim(new Claim(ClaimTypes.Name, authSession.DisplayName));
            identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, authSession.UserAuthName));

            foreach (var roleName in authSession.Roles)
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, roleName));
            }
            
            var principal = new ClaimsPrincipal();
            principal.AddIdentity(identity);

            return principal;
        }
    }
}