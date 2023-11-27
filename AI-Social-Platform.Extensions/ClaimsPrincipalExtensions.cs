namespace AI_Social_Platform.Extensions
{
    using System.Security.Claims;

    public static class ClaimsPrincipalExtensions
    {
        public static string GetUserId(this ClaimsPrincipal principal)
        {
            if (principal == null)
                throw new ArgumentNullException(nameof(principal));

            var loggedInUserId = principal.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (Guid.TryParse(loggedInUserId, out Guid userId))
            {
                return userId.ToString();
            }

            return Guid.Empty.ToString(); }
    }
}