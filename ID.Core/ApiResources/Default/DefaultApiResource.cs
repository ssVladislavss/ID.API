using IdentityModel;
using IdentityServer4.Models;

namespace ID.Core.ApiResources.Default
{
    public class DefaultApiResource
    {
        public static IDApiResource[] Resources
            => new[] { ServiceID, ServiceIDUI };
        public static IDApiResource ServiceID
            => new(0, new("service_id_api", "Service_ID_API")
            {
                Scopes = { "service_id_api" },
                ApiSecrets = { new Secret("EFDFE86821574858940A162AE47534EA".ToSha256()) },
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
            => new(0, new("service_id_ui", "Service_ID_UI")
            {
                Scopes = { "service_id_api" },
                ApiSecrets = { new Secret("db35631558bd4fbbb025f1a9d0dfa00d".ToSha256()) },
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
