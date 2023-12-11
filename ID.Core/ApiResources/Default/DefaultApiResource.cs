using IdentityModel;
using IdentityServer4.Models;

namespace ID.Core.ApiResources.Default
{
    public class DefaultApiResource
    {
        public static IDApiResource[] Resources
            => new[] { ServiceID, ServiceIDUI };
        public static IDApiResource ServiceID
            => new(0, new(IDConstants.ApiResources.Default.Names.ServiceIDApiName, "Service_ID_API")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.ServiceIDApiName },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.ServiceIDApiSecret.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource ServiceIDUI
            => new(0, new(IDConstants.ApiResources.Default.Names.ServiceIDUIName, "Service_ID_UI")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.ServiceIDUIName },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.ServiceIDUISecret.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
    }
}
