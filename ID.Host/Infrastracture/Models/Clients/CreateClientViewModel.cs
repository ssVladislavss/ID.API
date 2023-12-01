using IdentityServer4.Models;
using IdentityServer4;
using System.ComponentModel.DataAnnotations;

namespace ID.Host.Infrastracture.Models.Clients
{
    public class CreateClientViewModel
    {
        public string? ProtocolType { get; set; } = IdentityServerConstants.ProtocolTypes.OpenIdConnect;
        public List<Secret> ClientSecrets { get; set; } = new List<Secret>();
        public bool RequireClientSecret { get; set; } = true;
        [Required(ErrorMessage = "Поле - ClientName - обязательно к заполнению")]
        public string? ClientName { get; set; }
        public string? Description { get; set; }
        public string? ClientUri { get; set; }
        public string? LogoUri { get; set; }
        public bool RequireConsent { get; set; } = false;
        public bool AllowRememberConsent { get; set; } = true;
        public List<string> AllowedGrantTypes { get; set; } = new List<string>();
        public bool RequirePkce { get; set; } = true;
        public bool AllowPlainTextPkce { get; set; } = false;
        public bool RequireRequestObject { get; set; } = false;
        public bool AllowAccessTokensViaBrowser { get; set; } = false;
        public List<string> RedirectUris { get; set; } = new List<string>();
        public List<string> PostLogoutRedirectUris { get; set; } = new List<string>();
        public string? FrontChannelLogoutUri { get; set; }
        public bool FrontChannelLogoutSessionRequired { get; set; } = true;
        public string? BackChannelLogoutUri { get; set; }
        public bool BackChannelLogoutSessionRequired { get; set; } = true;
        public bool AllowOfflineAccess { get; set; } = false;
        public List<string> AllowedScopes { get; set; } = new List<string>();
        public bool AlwaysIncludeUserClaimsInIdToken { get; set; } = false;
        public int IdentityTokenLifetime { get; set; } = 300;
        public List<string> AllowedIdentityTokenSigningAlgorithms { get; set; } = new List<string>();
        public int AccessTokenLifetime { get; set; } = 3600;
        public int AuthorizationCodeLifetime { get; set; } = 300;
        public int AbsoluteRefreshTokenLifetime { get; set; } = 2592000;
        public int SlidingRefreshTokenLifetime { get; set; } = 1296000;
        public int? ConsentLifetime { get; set; } = null;
        public TokenUsage RefreshTokenUsage { get; set; } = TokenUsage.OneTimeOnly;
        public bool UpdateAccessTokenClaimsOnRefresh { get; set; } = false;      
        public TokenExpiration RefreshTokenExpiration { get; set; } = TokenExpiration.Absolute;
        public AccessTokenType AccessTokenType { get; set; } = AccessTokenType.Jwt;
        public bool EnableLocalLogin { get; set; } = true;
        public List<string> IdentityProviderRestrictions { get; set; } = new List<string>();
        public bool IncludeJwtId { get; set; } = true;
        public List<ClientClaimViewModel> Claims { get; set; } = new List<ClientClaimViewModel>();
        public bool AlwaysSendClientClaims { get; set; } = false;
        public string? ClientClaimsPrefix { get; set; } = "client_";
        public string? PairWiseSubjectSalt { get; set; }
        public int? UserSsoLifetime { get; set; }
        public string? UserCodeType { get; set; }
        public int DeviceCodeLifetime { get; set; } = 300;
        public List<string> AllowedCorsOrigins { get; set; } = new List<string>();
    }
}
