namespace AI_Social_Platform.Common
{
    public static class EntityValidationConstants
    {
        public static class User
        {
            public const int PasswordMinLength = 6;
            public const int PasswordMaxLength = 100;

            public const int FirstNameMinLength = 1;
            public const int FirstNameMaxLength = 15;

            public const int LastNameMinLength = 1;
            public const int LastNameMaxLength = 15;

            public const int PhoneNumberMaxLength = 10;
            public const int PhoneNumberMinLength = 8;
        }
    }
}