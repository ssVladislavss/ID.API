namespace ID.Core
{
    public class IDConstants
    {
        public static class Client
        {
            public static class Default
            {
                public static class Ids
                {
                    public const string ServiceIDApiId = "9C014C46-7A09-49A5-8264-99CD83495D28";
                    public const string ServiceIDUIId = "32c2c3a8-b8ed-4cbd-8e36-c8312fab0cc2";
                    public const string OnlineSaleAdminPanel = "480be209-dd15-4e94-a7b4-0ebef1d399c2";
                    public const string OnlineSaleApiGateway = "884e3fe2-b04e-4fb3-88aa-8943713583a1";
                }
                public static class Names
                {
                    public const string ServiceIdApi = "Сервис идентификации";
                    public const string ServiceIdAdministrationUI = "Панель управления сервисом идентификации";
                    public const string OnlineSaleAdminPanel = "Панель управления сервисом онлайн продаж";
                    public const string OnlineSaleApiGateway = "Входные точки управления сервисом онлайн продаж";
                }
                public static class Secrets
                {
                    public const string ServiceIdApiSecret = "4A534858408B41FFADE3FBC533CEE00E";
                    public const string ServiceIdUISecret = "330c4cee9b674891b02ead42cc4a4d89";
                    public const string OnlineSaleAdminPanel = "7ff297c4975941ed8a1c0d48aaea5148";
                    public const string OnlineSaleApiGateway = "74739efac6bd4f9994eb68a3664a1a6f";
                }
            }
            public static class Claims
            {
                public static class Types
                {
                    public const string ClientType = "type";
                    public const string ClientName = "name";
                }
                public static class Values
                {
                    public const string Additional = "additional";
                    public const string Base = "base";
                }
            }
        }
        public static class ApiScopes
        {
            public static class Default
            {
                public static class Names
                {
                    public const string ServiceIDApiName = "service_id_api";
                    public const string ServiceIDUIName = "service_id_administration_ui";
                    public const string OnlineSaleAdminPanel = "online_sales_admin_panel";
                    public const string OnlineSaleApiGateway = "online_sales_api_gateway";
                    public const string OnlineSaleFiscalHost = "online_sales_fiscal";
                    public const string OnlineSaleOrderHost = "online_sales_order";
                    public const string OnlineSalePaymentHost = "online_sales_payment";
                    public const string OnlineSaleOrganizationHost = "online_sales_organization";
                    public const string OnlineSaleSiteHost = "online_sales_site";
                    public const string OnlineSaleLoyaltyHost = "online_sales_loyalty";
                    public const string OnlineSaleProductHost = "online_sales_product";
                    public const string OnlineSaleEmailHost = "online_sales_email";
                    public const string OnlineSaleSmsHost = "online_sales_sms";
                }
            }
        }
        public static class ApiResources
        {
            public static class Default
            {
                public static class Names
                {
                    public const string ServiceIDApiName = "service_id_api";
                    public const string ServiceIDUIName = "service_id_administration_ui";

                    public const string OnlineSaleAdminPanel = "online_sales_admin_panel";
                    public const string OnlineSaleApiGateway = "online_sales_api_gateway";
                    public const string OnlineSaleFiscalHost = "online_sales_fiscal";
                    public const string OnlineSaleOrderHost = "online_sales_order";
                    public const string OnlineSalePaymentHost = "online_sales_payment";
                    public const string OnlineSaleOrganizationHost = "online_sales_organization";
                    public const string OnlineSaleSiteHost = "online_sales_site";
                    public const string OnlineSaleLoyaltyHost = "online_sales_loyalty";
                    public const string OnlineSaleProductHost = "online_sales_product";
                    public const string OnlineSaleEmailHost = "online_sales_email";
                    public const string OnlineSaleSmsHost = "online_sales_sms";
                }
                public static class Secrets
                {
                    public const string ServiceIDApiSecret = "EFDFE86821574858940A162AE47534EA";
                    public const string ServiceIDUISecret = "db35631558bd4fbbb025f1a9d0dfa00d";
                    public const string OnlineSaleAdminPanel = "fb9e89304c984669965963d188fd28cf";
                    public const string OnlineSaleApiGateway = "ebf5f740483742a2b405bd5e26918ac7";

                    public const string OnlineSaleFiscalHost = "9ed0cd3488244de38e557314ef6a0b1a";
                    public const string OnlineSaleOrderHost = "f6e362cc93b149da98bffccc281f6969";
                    public const string OnlineSalePaymentHost = "05f1b60a-b828457a9690cbef3fc5a624";
                    public const string OnlineSaleOrganizationHost = "c2dab2e4f97545c2a1b36a6590486c77";
                    public const string OnlineSaleSiteHost = "6b51e55b3f9e4aad8849d42c20018c67";
                    public const string OnlineSaleLoyaltyHost = "1c51793512e940a2914eceb4a4476d55";
                    public const string OnlineSaleProductHost = "8e42e3a6afab475799bd809861784a48";
                    public const string OnlineSaleEmailHost = "89496f2bdf4c481fbb00fb75ac7fba22";
                    public const string OnlineSaleSmsHost = "71266cafebf14670b09d4ec1753d0d7c";
                }
            }
        }
        public static class Roles
        {
            public const string RootAdmin = "root_admin";
            public const string Admin = "admin";

            public static class Claims
            {
                public static class Types
                {
                    public const string RoleType = "role_type";
                }
                public static class Values
                {
                    public const string Additional = "additional";
                    public const string Base = "base";
                }
            }
        }
        public static class Users
        {
            public static class ConfirmationCodeProviders
            {
                public const string IDProvider = "identity_server";
            }
            public static class CodeNames
            {
                public const string ConfirmationCode = "confirmation_code";
                public const string CodeBySetLockoutEnabled = "confirmation_code_to_lock";
            }
        }
    }
}
