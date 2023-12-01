using ID.Core.ApiResources;
using IdentityModel;
using IdentityServer4.Models;

namespace ID.Core.ApiResources.Default
{
    public class DefaultApiResource
    {
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
    }
}
