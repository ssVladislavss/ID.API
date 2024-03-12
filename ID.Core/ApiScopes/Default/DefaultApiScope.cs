using IdentityModel;
using IdentityServer4.Models;

namespace ID.Core.ApiScopes.Default
{
    public class DefaultApiScope
    {
        public static ApiScope[] Scopes
            => new[]
            { 
                ServiceID,
                ServiceIDUI,
                OnlineSaleApiGateway,
                OnlineSaleAdminPanel,
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
        public static ApiScope ServiceID
            => new(IDConstants.ApiScopes.Default.Names.ServiceIDApiName, "Service_ID_API",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope ServiceIDUI
            => new(IDConstants.ApiScopes.Default.Names.ServiceIDUIName, "Service_ID_UI",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSaleAdminPanel
            => new(IDConstants.ApiScopes.Default.Names.OnlineSaleAdminPanel, "Панель администратора сервиса онлайн продаж",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSaleApiGateway
            => new(IDConstants.ApiScopes.Default.Names.OnlineSaleApiGateway, "Конечные точки управления сервисом онлайн продаж",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });

        public static ApiScope OnlineSalesFiscalHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleFiscalHost, "Сервис фискализации",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesLoyaltyHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleLoyaltyHost, "Сервис лояльности",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesPaymentHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSalePaymentHost, "Сервис проведения платежей",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesOrderHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleOrderHost, "Сервис обработки заказов",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesOrganizationHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleOrganizationHost, "Сервис организаций",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesSiteHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleSiteHost, "Сервис управления сайтами",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesProductHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleProductHost, "Сервис управления услугами",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesEmailHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleEmailHost, "Сервис управления email рассылкой",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesSmsHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleSmsHost, "Сервис управления смс рассылкой",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
        public static ApiScope OnlineSalesCartHost
            => new(IDConstants.ApiResources.Default.Names.OnlineSaleCartHost, "Сервис управления корзиной клиента",
                    new[]
                    {
                        JwtClaimTypes.Name,
                        JwtClaimTypes.Role,
                        JwtClaimTypes.Email,
                        JwtClaimTypes.Subject,
                        JwtClaimTypes.PreferredUserName
                    });
    }
}
