using Domain.Interfaces.Interfaceservices;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;


namespace Services
{
    public class UserLoggedService : IUserLogged
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public UserLoggedService(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public int GetCurrentUserId()
        {
            return int.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
        }
    }
}
