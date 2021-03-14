using System.Linq;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Jt.CustomAuthScheme.Api.Controllers
{
    [AllowAnonymous]
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        [HttpGet]
        public IActionResult Get()
        {
            var currentUser = HttpContext.User;
            return Ok(new
            {
                Name = currentUser.Identity?.Name,
                AuthenticationType = currentUser.Identity?.AuthenticationType,
                IsAuthenticated = currentUser.Identity?.IsAuthenticated,
                Roles = currentUser.FindAll(ClaimTypes.Role).Select(c => c.Value)
            });
        }
    }
}