namespace Common
{
    public static class ExceptionMessages
    {
        public static class Animal
        {
            public const string InvalidNameLength = "The animal name should be with length between 2 and 50";
            public const string InvalidDescriptionLength = "The animal description has max length of 150";
            public const string AnimalNotFound = "The animal is not found";
            public const string SameAnimal = "Animal IDs must be different.";
        }

        public static class Match
        {
            public const string AlreadyMatched = "There is already a match between these animals";
            public const string MatchNotFound = "The animals are not matched";
        }

        public static class AnimalCategory
        {
            public const string InvalidNameLength = "The animal category should be with length between 3 and 30";
        }

        public static class Breed
        {
            public const string InvalidNameLength = "The animal breed should be with length between 5 and 30";
        }

        public static class Passion
        {
            public const string InvalidNameLength = "The passion should be with length between 5 and 30";
        }

        public static class User
        {
            public const string InvalidNameLength = "The user name should be with length between 2 and 100";
            public const string InvalidDescriptionLength = "The user description have max length of 150";
            public const string InvalidEducationLength = "The user education should be with length between 5 and 50";
            public const string InvalidJobTitleLength = "The user job title should be with length between 5 and 50";
            public const string InvalidAddressLength = "The user address should be with length between 10 and 150";
            public const string InvalidCityLength = "The user city should be with length between 3 and 50";
            public const string UserNotFound = "The user is not found";
            public const string UserResultNotSucceeded = "The user result is not succeeded";
            public const string InvalidLogin = "Invalid email or password";
            public const string InvalidPassword = "THe password is invalid";
            public const string InvalidPasswordLength = "The password must be at least 5 characters long";
            public const string PasswordsDoNotMatch = "Passwords do not match";
            public const string InvalidEmail = "The email is invalid";
            public const string InvalidRegister = "User registration failed";
            public const string NotAuthenticated = "The user is not authenticated";
        }

        public static class Message
        {
            public const string InvalidContentLength = "The message content has max length of 350";
        }

        public static class Repository
        {
            public const string EntityNotFound = "Entity not found";
        }

        public static class Entity
        {
            public const string InvalidGuidFormat = "The id is not a valid GUID format";
            public const string InternalServerError = "Internal Server Error";
        }
    }
}
