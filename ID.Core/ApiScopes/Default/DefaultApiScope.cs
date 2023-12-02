using IdentityModel;
using IdentityServer4.Models;

namespace ID.Core.ApiScopes.Default
{
    public class DefaultApiScope
    {
        public static ApiScope[] Scopes
            => new[] { ServiceID, ServiceIDUI };
        public static ApiScope ServiceID
            => new("service_id_api", "Service_ID_API",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope ServiceIDUI
            => new("service_id_ui", "Service_ID_UI",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
    }
}
