namespace Common
{
    public static class EntityValidationConstants
    {
        public static class Animal
        {
            public const int NameMinLength = 2;
            public const int NameMaxLength = 50;
            public const int DescriptionMaxLength = 150;
            public const string AgeMinValue = "1";
            public const string AgeMaxValue = "20";
            public const int SocialMediaMinLength = 4;
            public const string WeigthMinValue = "1";
            public const string WeigthMaxValue = "100";
        }

        public static class User
        {
            public const string AgeMinValue = "16";
            public const string AgeMaxValue = "90";
            public const int NameMinLength = 2;
            public const int NameMaxLength = 100;
            public const int DescriptionMaxLength = 150;
            public const int AddressMinLength = 10;
            public const int AddressMaxLength = 150;
            public const int CityMinLength = 3;
            public const int CityMaxLength = 50;
            public const int EducationMinLength = 5;
            public const int EducationMaxLength = 50;
            public const int JobTitleMinLength = 5;
            public const int JobTitleMaxLength = 50;
        }

        public static class Message
        {
            public const int ContentMaxLength = 350;
        }

        public static class Passion
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 30;
        }

        public static class Breed
        {
            public const int NameMinLength = 5;
            public const int NameMaxLength = 30;
        }

        public static class AnimalCategory
        {
            public const int NameMinLength = 3;
            public const int NameMaxLength = 30;
        }
    }
}