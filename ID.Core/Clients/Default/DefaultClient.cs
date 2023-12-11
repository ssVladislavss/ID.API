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
                ClientName = IDConstants.Client.Default.Names.ServiceIdApiName,
                Enabled = true,
                Claims = {
                    new(IDConstants.Client.Claims.Types.ClientType, IDConstants.Client.Claims.Values.Base),
                    new(IDConstants.Client.Claims.Types.ClientName, IDConstants.Client.Default.Names.ServiceIdApiName)
                },

                ClientId = IDConstants.Client.Default.Ids.ServiceIDApiId,
                ClientSecrets = { new Secret(IDConstants.Client.Default.Secrets.ServiceIdApiSecret.ToSha256(), IDConstants.Client.Default.Secrets.ServiceIdApiSecret) },
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
                    IDConstants.ApiScopes.Default.Names.ServiceIDApiName,
                    IDConstants.ApiScopes.Default.Names.ServiceIDUIName
                }
            };
        public static Client ServiceIDUI
            => new()
            {
                ClientName = IDConstants.Client.Default.Names.ServiceIdUIName,
                Enabled = true,
                Claims = {
                    new(IDConstants.Client.Claims.Types.ClientType, IDConstants.Client.Claims.Values.Base),
                    new(IDConstants.Client.Claims.Types.ClientName, IDConstants.Client.Default.Names.ServiceIdUIName)
                },

                ClientId = IDConstants.Client.Default.Ids.ServiceIDUIId,
                ClientSecrets = { new Secret(IDConstants.Client.Default.Secrets.ServiceIdUISecret.ToSha256(), IDConstants.Client.Default.Secrets.ServiceIdUISecret) },
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
                    IDConstants.ApiScopes.Default.Names.ServiceIDApiName,
                    IDConstants.ApiScopes.Default.Names.ServiceIDUIName
                }
            };
    }
}
