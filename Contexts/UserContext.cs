using System.Security.Claims;

namespace dotnet_rpg.Contexts
{
    public class UserContext : IUserContext
    {
        private readonly IHttpContextAccessor _httpContextAcessor;
        public UserContext(IHttpContextAccessor httpContextAcessor)
        {
            _httpContextAcessor = httpContextAcessor;
        }

        public int GetUserId() => int.Parse(_httpContextAcessor.HttpContext!.User
            .FindFirstValue(ClaimTypes.NameIdentifier)!);
    }
}