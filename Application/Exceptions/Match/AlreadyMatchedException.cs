namespace Application.Exceptions.Match
{
    using static Common.ExceptionMessages.Match;

    public class AlreadyMatchedException : Exception
    {
        public AlreadyMatchedException()
            : base(AlreadyMatched) { }

        public AlreadyMatchedException(string message)
            : base(message) { }
    }
}
