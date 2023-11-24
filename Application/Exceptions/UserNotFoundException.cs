namespace Application.Exceptions
{
    using static Common.ExceptionMessages.User;

    public class UserNotFoundException : Exception
    {
        public UserNotFoundException() 
            :base(UserNotFound) { }

        public UserNotFoundException(string message) 
            :base(message) { }
    }
}
