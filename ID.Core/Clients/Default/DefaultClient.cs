using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace ID.Core.Clients.Default
{
    public class DefaultClient
    {
        public static Client[] Clients
            => new[] { ServiceID, ServiceIDUI };
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
                AllowedCorsOrigins = { "https://localhost:44338", "https://localhost:44333" },
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
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "service_id_api",
                    "service_id_ui"
                }
            };
        public static Client ServiceIDUI
            => new()
            {
                ClientName = "Service ID UI",
                Enabled = true,
                Claims = {
                    new(IDConstants.Client.Claims.ClientType, "base"),
                    new(IDConstants.Client.Claims.ClientName, "Service ID UI")
                },

                ClientId = "32c2c3a8-b8ed-4cbd-8e36-c8312fab0cc2",
                ClientSecrets = { new Secret("330c4cee9b674891b02ead42cc4a4d89".ToSha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedCorsOrigins = { "https://localhost:44333" },
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
                    IdentityServerConstants.StandardScopes.OfflineAccess,
                    "service_id_api",
                    "service_id_ui"
                }
            };
    }
}
