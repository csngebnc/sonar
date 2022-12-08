namespace Shipping.Api.Common
{
    public static class ApiResources
    {
        public const string BasePath = "api";

        public static class Locker
        {
            public const string BasePath = ApiResources.BasePath + "/locker";
        }

        public static class ExtraOption
        {
            public const string BasePath = ApiResources.BasePath + "/extra";
        }

        public static class User
        {
            public const string BasePath = ApiResources.BasePath + "/user";
        }
    }
}
