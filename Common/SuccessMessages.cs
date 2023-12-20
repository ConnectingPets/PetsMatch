namespace Common
{
    public static class SuccessMessages
    {
        public static class Match
        {
            public const string SuccessMatch = "The animals are successfully matched";
            public const string SuccessUnMatch = "The animals are successfully unmatched";
        }

        public static class User
        {
            public const string SuccessDeleteUser = "The user {0} is deleted successfully";
            public const string SuccessEditUser = "The user {0} is edited successfully";
            public const string SuccessDeleteRole = 
                "The profile {0} is deleted successfully";
        }

        public static class Animal
        {
            public const string SuccessfullyAddedAnimal = "You have successfully add {0} to your pet's list";
            public const string SuccessfullyDeleteAnimal = "You have successfully delete {0} from your pet's list";
            public const string SuccessfullyEditAnimal = "Successfully updated {0}";
        }

        public static class Photo
        {
            public const string SuccessfullyUploadPhoto = "Successfully upload photo";
            public const string SuccessfullyDeletedPhoto = "Successfully delete photo";
            public const string SuccessfullySetMainPhoto = "You successfully set main photo";
        }
    }
}
