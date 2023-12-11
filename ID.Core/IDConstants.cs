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
                }
                public static class Names
                {
                    public const string ServiceIdApiName = "Service ID API";
                    public const string ServiceIdUIName = "Service UI API";
                }
                public static class Secrets
                {
                    public const string ServiceIdApiSecret = "4A534858408B41FFADE3FBC533CEE00E";
                    public const string ServiceIdUISecret = "330c4cee9b674891b02ead42cc4a4d89";
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
                    public const string ServiceIDUIName = "service_id_ui";
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
                    public const string ServiceIDUIName = "service_id_ui";
                }
                public static class Secrets
                {
                    public const string ServiceIDApiSecret = "EFDFE86821574858940A162AE47534EA";
                    public const string ServiceIDUISecret = "db35631558bd4fbbb025f1a9d0dfa00d";
                }
            }
        }
        public static class Roles
        {
            public const string RootAdmin = "root_admin";

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
            public static class VerifyCodeTypes
            {
                public const string CodeOnEmail = "code_on_email";
                public const string CodeOnPhone = "code_on_phone";
            }
        }
    }
}
