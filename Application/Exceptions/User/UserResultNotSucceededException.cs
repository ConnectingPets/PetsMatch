namespace Application.Exceptions.User
{
    using static Common.ExceptionMessages.User;

    public class UserResultNotSucceededException : Exception
    {
        public UserResultNotSucceededException()
            : base(UserResultNotSucceeded) { }

        public UserResultNotSucceededException(string message)
            : base(message) { }
    }
}
