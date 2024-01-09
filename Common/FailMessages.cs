namespace Common
{
    public static class FailMessages
    {
        public static class User
        {
            public const string FailedDeleteUser = "Failed to delete the user {0}";
            public const string FailedEditUser = "Failed to edit the user {0}";
            public const string FailedLogin = "Invalid email or password";
            public const string FailedRegister = "User registration failed";
            public const string FailedLogout = "Failed to logout";
            public const string FailedAddUserPhoto = "Failed to add {0}'s photo";
            public const string FailedDeleteUserPhoto = "Failed to delete {0}'s photo";
            public const string FailedDeleteRole = 
                "Failed to delete {0} profile";
        }

        public static class Match
        {
            public const string FailedMatch = "Failed to match the animals";
            public const string FailedUnMatch = "Failed to unmatch the animals";
        }

        public static class Message
        {
            public const string FailedSendMessage = "Failed to send the message";
        }

        public static class Swipe
        {
            public const string FailedSwipe = "Failed to swipe on the animal";
        }
    }
}
