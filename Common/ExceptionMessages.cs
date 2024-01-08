namespace Common
{
    public static class ExceptionMessages
    {
        public static class Animal
        {
            public const string InvalidNameLength = "The animal name should be with length between 2 and 50";
            public const string InvalidDescriptionLength = "The animal description has max length of 150";
            public const string AnimalNotFound = "This pet does not exist! Please select an existing one";
            public const string NotOwner = "This pet does not belong to you";
            public const string FailedToCreate = "Failed to create pet - {0}";
            public const string NoPets = "You don't have pets yet";
            public const string NotRightUser = "This pet does not belong to you!";
            public const string FailedToDelete = "Failed to delete pet - {0}";
            public const string FailedToEdit = "Failed to edit pet - {0}";
            public const string CannotUpdateName = "Can not update pet name for another {0} days.";
            public const string CannotUpdateBreed = "Can not update pet breed for another {0} days.";
            public const string CannotUpdateGender = "Can not update pet gender for another {0} days.";
        }

        public static class Match
        {
            public const string AlreadyMatched = "There is already a match between these animals";
            public const string MatchNotFound = "The animals are not matched";
            public const string SameAnimal = "The animal is matching on itself";
            public const string NotMatched = "The animals don't like each other";
        }

        public static class Swipe
        {
            public const string SameAnimal = "The animal is swiping on itself";
        }

        public static class AnimalCategory
        {
            public const string InvalidNameLength = "The animal category should be with length between 3 and 30";
            public const string CategoryNotExist = "This category does not exist. Please select existing one";
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
            public const string InvalidPassword = "THe password is invalid";
            public const string InvalidPasswordLength = "The password must be at least 5 characters long";
            public const string PasswordsDoNotMatch = "Passwords do not match";
            public const string InvalidEmail = "The email is invalid";
            public const string NotAuthenticated = "The user is not authenticated";
            public const string InvalidGender = "The gender that you picked is not valid";
        }

        public static class Message
        {
            public const string InvalidContentLength = "The message content has max length of 350";
        }

        public static class Repository
        {
            public const string EntityNotFound = "Entity not found";
        }

        public static class Marketplace
        {
            public const string NoAnimalsForAdoption = "We still don't have animal for adoption";
            public const string NoAnimalsForSale = "We still don't have animals for sale";
            public const string DoNotHaveAnimalForSale = "You still don't have animal for sale";
            public const string DoNotHaveAnimalForAdoption = "You still don't have animal for adoption";
        }

        public static class Photo
        {
            public const string EmptyPhoto = "File is not selected or empty";
            public const string AlreadyHavePhoto = "You already have photo!";
            public const string NotImage = "This file is not an image";
            public const string PhotoNotExist = "This photo does not exist! Please select existing one";
            public const string MainPhotoError = "This is your main photo! You can not delete it";
            public const string DonNotHavePhoto = "You don't have photo yet!";
            public const string ErrorUploadPhoto = "Error occurred during uploading photo";
            public const string FullCapacityImage = "You already have 6 photos of this animal. You cannot add more";
            public const string FailedToDeletePhoto = "Failed to delete photo";
            public const string ErrorDeletingPhoto = "Error occurred during deleting photo";
            public const string ErrorSetMain = "Error occurred during set main";
        }

        public static class Entity
        {
            public const string InvalidGuidFormat = "This is invalid Guid format";
        }
    }
}
