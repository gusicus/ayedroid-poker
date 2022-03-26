using System.Security.Claims;

namespace Ayedroid.Poker.App.Utilities
{
    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            string? id = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);

            if (id == null)
                throw new ArgumentException($"User is missing {ClaimTypes.NameIdentifier}");

            return id;
        }

        public static string GetUserName(this ClaimsPrincipal claimsPrincipal)
        {
            string? name = claimsPrincipal.FindFirstValue(ClaimTypes.Name);

            if (name == null)
                throw new ArgumentException($"User is missing {ClaimTypes.Name}");

            return name;
        }
    }
}
