namespace AI_Social_Platform.Common
{
    public static class GeneralApplicationConstants
    {
        public const int ReleaseYear = 2023;

        public const int DefaultPage = 1;
        public const int EntitiesPerPage = 10;

        public const string AdminAreaName = "Admin";
        public const string AdminRoleName = "Administrator";
        public const string DevelopmentAdminEmail = "admin@admin.com";

        public const string UserCacheKey = "UsersCache";
        public const string ReservationsCacheKey = "ReservationsCache";
        public const int UsersCacheDurationMinutes = 5;
        public const int ReservationCacheDurationMinutes = 10;

        public const string OnlineUsersCookieName = "OnlineUsers";
        public const int LastActivityBeforeOfflineMinutes = 10;
    }
}
