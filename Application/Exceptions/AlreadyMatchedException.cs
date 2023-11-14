namespace Application.Exceptions
{
    public class AlreadyMatchedException : Exception
    {
        public AlreadyMatchedException(string message)
            :base(message) { }
    }
}
