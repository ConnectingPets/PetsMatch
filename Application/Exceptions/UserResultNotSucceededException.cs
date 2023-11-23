namespace Application.Exceptions
{
    public class UserResultNotSucceededException : Exception
    {
        public UserResultNotSucceededException()
            :base() { }

        public UserResultNotSucceededException(string message)
            :base(message) { }
    }
}
