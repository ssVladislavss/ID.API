using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace ID.Core.Clients.Default
{
    public class DefaultClient
    {
        public static Client ServiceID
            => new()
            {
                ClientName = "Service ID API",
                Enabled = true,
                Claims = {
                    new(IDConstants.Client.Claims.ClientType, "base"),
                    new(IDConstants.Client.Claims.ClientName, "Service ID API")
                },

                ClientId = "9C014C46-7A09-49A5-8264-99CD83495D28",
                ClientSecrets = { new Secret("4A534858408B41FFADE3FBC533CEE00E".ToSha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedCorsOrigins = { },
                RedirectUris = { },
                PostLogoutRedirectUris = { },
                AlwaysIncludeUserClaimsInIdToken = true,
                RequireConsent = false,
                AllowOfflineAccess = true,
                AlwaysSendClientClaims = true,
                UpdateAccessTokenClaimsOnRefresh = true,
                AllowedScopes =
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    IdentityServerConstants.StandardScopes.OfflineAccess
                }
            };
    }
}
