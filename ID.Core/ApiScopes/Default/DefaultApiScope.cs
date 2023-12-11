using IdentityModel;
using IdentityServer4.Models;

namespace ID.Core.ApiScopes.Default
{
    public class DefaultApiScope
    {
        public static ApiScope[] Scopes
            => new[] { ServiceID, ServiceIDUI };
        public static ApiScope ServiceID
            => new(IDConstants.ApiScopes.Default.Names.ServiceIDApiName, "Service_ID_API",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope ServiceIDUI
            => new(IDConstants.ApiScopes.Default.Names.ServiceIDUIName, "Service_ID_UI",
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
