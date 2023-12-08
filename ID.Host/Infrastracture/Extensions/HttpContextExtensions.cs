using ID.Core;
using System.Security.Claims;

namespace ID.Host.Infrastracture.Extensions
{
    public static class HttpContextExtensions
    {
        public static Iniciator ToIniciator(this ClaimsPrincipal principal)
        {
            return new Iniciator(principal);
        }
    }
}
