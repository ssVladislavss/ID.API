namespace ID.Core
{
    public class IDConstants
    {
        public static class Client
        {
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
    }
}
