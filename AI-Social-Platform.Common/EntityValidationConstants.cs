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

        public static class School
        {
            public const int SchoolMaxLength = 50;
            public const int SchoolMinLength = 3;
        }

        public static class Country
        {
            public const int CountryMaxLength = 50;
            public const int CountryMinLength = 3;
        }

        public static class State
        {
            public const int StateMaxLength = 50;
            public const int StateMinLength = 3;
        }

        public static class MediaConstants
        {
            public const int MediaTitleMaxLength = 50;
            public const int MediaTitleMinLength = 2;
        }

        public static class Publication
        {
            public const int PublicationContentMaxLength = 600;
        }

        public static class Comment
        {
            public const int CommentContentMaxLength = 256;
        }
    }
}