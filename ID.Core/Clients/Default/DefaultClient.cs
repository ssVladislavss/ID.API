using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;
using Microsoft.Extensions.Configuration;

namespace ID.Core.Clients.Default
{
    public class DefaultClient
    {
        private static IConfiguration _configuration = default!;

        public DefaultClient(IConfiguration configuration)
        {
            _configuration = configuration ?? throw new ArgumentNullException(nameof(configuration));
        }

        public static Client[] Clients
            => new[] 
            { 
                ServiceID,
                ServiceIDUI,
                OnlineSaleAdminPanel,
                OnlineSaleApiGateway
            };
        public static Client ServiceID
            => new()
            {
                ClientName = IDConstants.Client.Default.Names.ServiceIdApi,
                Enabled = true,
                Claims = {
                    new(IDConstants.Client.Claims.Types.ClientType, IDConstants.Client.Claims.Values.Base),
                    new(IDConstants.Client.Claims.Types.ClientName, IDConstants.Client.Default.Names.ServiceIdApi)
                },

                ClientId = IDConstants.Client.Default.Ids.ServiceIDApiId,
                ClientSecrets = { new Secret(IDConstants.Client.Default.Secrets.ServiceIdApiSecret.ToSha256(), IDConstants.Client.Default.Secrets.ServiceIdApiSecret) },
                AllowedGrantTypes = GrantTypes.ImplicitAndClientCredentials,
                AllowedCorsOrigins = { "https://localhost:44338", "https://localhost:44333" },
                RedirectUris = { "https://localhost:44338/swagger/oauth2-redirect.html" },
                PostLogoutRedirectUris = { "https://localhost:44338/swagger/oauth2-redirect.html" },
                AlwaysIncludeUserClaimsInIdToken = true,
                AllowAccessTokensViaBrowser = true,
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
                ClientName = IDConstants.Client.Default.Names.ServiceIdAdministrationUI,
                Enabled = true,
                Claims = {
                    new(IDConstants.Client.Claims.Types.ClientType, IDConstants.Client.Claims.Values.Base),
                    new(IDConstants.Client.Claims.Types.ClientName, IDConstants.Client.Default.Names.ServiceIdAdministrationUI)
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
        public static Client OnlineSaleAdminPanel
            => new()
            {
                ClientName = IDConstants.Client.Default.Names.OnlineSaleAdminPanel,
                Enabled = true,
                Claims = {
                    new(IDConstants.Client.Claims.Types.ClientType, IDConstants.Client.Claims.Values.Base),
                    new(IDConstants.Client.Claims.Types.ClientName, IDConstants.Client.Default.Names.OnlineSaleAdminPanel)
                },

                ClientId = IDConstants.Client.Default.Ids.OnlineSaleAdminPanel,
                ClientSecrets = { new Secret(IDConstants.Client.Default.Secrets.OnlineSaleAdminPanel.ToSha256(), IDConstants.Client.Default.Secrets.OnlineSaleAdminPanel) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedCorsOrigins = { "https://localhost:7268" },
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
                    IDConstants.ApiScopes.Default.Names.OnlineSaleApiGateway,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleAdminPanel,

                    IDConstants.ApiScopes.Default.Names.OnlineSaleFiscalHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleLoyaltyHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSalePaymentHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleOrderHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleOrganizationHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleSiteHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleEmailHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleSmsHost,
                }
            };
        public static Client OnlineSaleApiGateway
            => new()
            {
                ClientName = IDConstants.Client.Default.Names.OnlineSaleApiGateway,
                Enabled = true,
                Claims = {
                    new(IDConstants.Client.Claims.Types.ClientType, IDConstants.Client.Claims.Values.Base),
                    new(IDConstants.Client.Claims.Types.ClientName, IDConstants.Client.Default.Names.OnlineSaleApiGateway)
                },

                ClientId = IDConstants.Client.Default.Ids.OnlineSaleApiGateway,
                ClientSecrets = { new Secret(IDConstants.Client.Default.Secrets.OnlineSaleApiGateway.ToSha256(), IDConstants.Client.Default.Secrets.OnlineSaleApiGateway) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPasswordAndClientCredentials,
                AllowedCorsOrigins = { "https://localhost:7181" },
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
                    IDConstants.ApiScopes.Default.Names.OnlineSaleApiGateway,

                    IDConstants.ApiScopes.Default.Names.OnlineSaleFiscalHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleLoyaltyHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSalePaymentHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleOrderHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleOrganizationHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleSiteHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleEmailHost,
                    IDConstants.ApiScopes.Default.Names.OnlineSaleSmsHost,
                }
            };
    }
}
