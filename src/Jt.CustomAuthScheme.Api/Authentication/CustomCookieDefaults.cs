namespace Jt.CustomAuthScheme.Api.Authentication
{
    public static class CustomCookieDefaults
    {
        public const string AuthenticationScheme = "CustomCookie";
        public const string TemporarySessionIdCookieName = "ss-id";
        public const string PermanentSessionIdCookieName = "ss-pid";
        public const string SessionOptionCookieName = "ss-opt";
    }
}