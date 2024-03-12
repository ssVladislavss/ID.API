using IdentityModel;
using IdentityServer4.Models;

namespace ID.Core.ApiResources.Default
{
    public class DefaultApiResource
    {
        public static IDApiResource[] Resources
            => new[] 
            { 
                ServiceID,
                ServiceIDUI,
                OnlineSalesAdminPanel,
                OnlineSalesApiGateway,
                OnlineSalesFiscalHost,
                OnlineSalesPaymentHost,
                OnlineSalesOrderHost,
                OnlineSalesLoyaltyHost,
                OnlineSalesOrganizationHost,
                OnlineSalesSiteHost,
                OnlineSalesProductHost,
                OnlineSalesEmailHost,
                OnlineSalesSmsHost,
                OnlineSalesCartHost
            };
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
        public static IDApiResource OnlineSalesAdminPanel
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleAdminPanel, "Панель администратора сервиса онлайн продаж")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleAdminPanel },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleAdminPanel.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesApiGateway
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleApiGateway, "Конечные точки управления сервисом онлайн продаж")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleApiGateway },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleApiGateway.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesFiscalHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleFiscalHost, "Сервис фискализации")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleFiscalHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleFiscalHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesLoyaltyHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleLoyaltyHost, "Сервис лояльности")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleLoyaltyHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleLoyaltyHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesPaymentHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSalePaymentHost, "Сервис проведения платежей")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSalePaymentHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSalePaymentHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesOrderHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleOrderHost, "Сервис обработки заказов")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleOrderHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleOrderHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesOrganizationHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleOrganizationHost, "Сервис организаций")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleOrganizationHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleOrganizationHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesSiteHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleSiteHost, "Сервис управления сайтами")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleSiteHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleSiteHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesProductHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleProductHost, "Сервис управления услугами")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleProductHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleProductHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesEmailHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleEmailHost, "Сервис управления email рассылкой")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleEmailHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleEmailHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesSmsHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleSmsHost, "Сервис управления смс рассылкой")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleSmsHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleSmsHost.ToSha256()) },
                UserClaims =
                {
                    JwtClaimTypes.Name,
                    JwtClaimTypes.Role,
                    JwtClaimTypes.Subject,
                    JwtClaimTypes.PreferredUserName,
                    JwtClaimTypes.Email
                }
            });
        public static IDApiResource OnlineSalesCartHost
            => new(0, new(IDConstants.ApiResources.Default.Names.OnlineSaleCartHost, "Сервис управления корзиной клиента")
            {
                Scopes = { IDConstants.ApiScopes.Default.Names.OnlineSaleCartHost },
                ApiSecrets = { new Secret(IDConstants.ApiResources.Default.Secrets.OnlineSaleCartHost.ToSha256()) },
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
