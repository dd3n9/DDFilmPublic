namespace DDFilm.Domain.Common.Constants
{
    public static class StaticApplicationUserRoles
    {
        public const string OWNER = "OWNER";
        public const string PREMIUM = "PREMIUM";
        public const string USER = "USER";

        public const string OwnerPremium = "OWNER,PREMIUM";
        public const string OwnerPremiumUser = "OWNER,PREMIUM,USER";
    }
}
