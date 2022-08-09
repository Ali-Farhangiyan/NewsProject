using System.Security.Claims;

namespace Admin.Utilities
{
    public static class ClaimUtility
    {
        public static string GetUserId(ClaimsPrincipal User)
        {
            var claimsIdentity = User.Identity as ClaimsIdentity;
            string userId = claimsIdentity
                .FindFirst(ClaimTypes.NameIdentifier).Value;

            return userId;
        }
    }
}
