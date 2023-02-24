namespace DynamicsPortal.Common
{
    using System;
    using System.Security.Claims;

    public static class UserExtensions
    {
        public static Guid GetContactId(this ClaimsPrincipal principal)
        {
            return Guid.Parse(principal.FindFirstValue(ClaimTypes.Sid));
        }
    }
}
