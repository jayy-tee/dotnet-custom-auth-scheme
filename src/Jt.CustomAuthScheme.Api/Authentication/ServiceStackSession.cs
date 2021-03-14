using System;
using System.Collections.Generic;

namespace Jt.CustomAuthScheme.Api.Authentication
{
    public class ServiceStackSession
    {
        public string Id { get; set; }
        public string UserAuthId { get; set; }
        public string UserAuthName { get; set; }
        public string UserName { get; set; }
        public string DisplayName { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public List<string> Roles { get; set; }
        public string AuthProvider { get; set; }
    }
}