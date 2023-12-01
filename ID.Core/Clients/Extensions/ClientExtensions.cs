using IdentityServer4.Models;

namespace ID.Core.Clients.Extensions
{
    public static class ClientExtensions
    {
        public static void Set(this Client inner, Client source)
        {
            inner.ClientUri = source.ClientUri;
            inner.AbsoluteRefreshTokenLifetime = source.AbsoluteRefreshTokenLifetime;
            inner.AccessTokenLifetime = source.AccessTokenLifetime;
            inner.AccessTokenType = source.AccessTokenType;
            inner.AllowAccessTokensViaBrowser = source.AllowAccessTokensViaBrowser;
            inner.AllowedCorsOrigins = source.AllowedCorsOrigins;
            inner.AllowedScopes = source.AllowedScopes;
            inner.AllowedGrantTypes = source.AllowedGrantTypes;
            inner.AllowedIdentityTokenSigningAlgorithms = source.AllowedIdentityTokenSigningAlgorithms;
            inner.AllowOfflineAccess = source.AllowOfflineAccess;
            inner.AllowPlainTextPkce = source.AllowPlainTextPkce;
            inner.AllowRememberConsent = source.AllowRememberConsent;
            inner.AlwaysIncludeUserClaimsInIdToken = source.AlwaysIncludeUserClaimsInIdToken;
            inner.AlwaysSendClientClaims = source.AlwaysSendClientClaims;
            inner.AuthorizationCodeLifetime = source.AuthorizationCodeLifetime;
            inner.BackChannelLogoutSessionRequired = source.BackChannelLogoutSessionRequired;
            inner.BackChannelLogoutUri = source.BackChannelLogoutUri;
            inner.Claims = source.Claims;
            inner.Enabled = source.Enabled;
            inner.EnableLocalLogin = source.EnableLocalLogin;
            inner.FrontChannelLogoutSessionRequired = source.FrontChannelLogoutSessionRequired;
            inner.FrontChannelLogoutUri = source.FrontChannelLogoutUri;
            inner.IdentityProviderRestrictions = source.IdentityProviderRestrictions;
            inner.IdentityTokenLifetime = source.IdentityTokenLifetime;
            inner.IncludeJwtId = source.IncludeJwtId;
            inner.LogoUri = source.LogoUri;
            inner.PairWiseSubjectSalt = source.PairWiseSubjectSalt;
            inner.PostLogoutRedirectUris = source.PostLogoutRedirectUris;
            inner.Properties = source.Properties;
            inner.ProtocolType = source.ProtocolType;
            inner.RedirectUris = source.RedirectUris;
            inner.RefreshTokenExpiration = source.RefreshTokenExpiration;
            inner.RefreshTokenUsage = source.RefreshTokenUsage;
            inner.RequireClientSecret = source.RequireClientSecret;
            inner.RequireConsent = source.RequireConsent;
            inner.RequirePkce = source.RequirePkce;
            inner.RequireRequestObject = source.RequireRequestObject;
            inner.SlidingRefreshTokenLifetime = source.SlidingRefreshTokenLifetime;
            inner.UpdateAccessTokenClaimsOnRefresh = source.UpdateAccessTokenClaimsOnRefresh;
            inner.UserCodeType = source.UserCodeType;
            inner.UserSsoLifetime = source.UserSsoLifetime;
            inner.ClientName = source.ClientName;
            inner.ClientSecrets = source.ClientSecrets;
        }
    }
}
