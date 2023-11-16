namespace Application.Exceptions
{
    using static Common.ExceptionMessages.Match;

    public class MatchNotFoundException : Exception
    {
        public MatchNotFoundException() 
            :base(MatchNotFound) { }

        public MatchNotFoundException(string message)
            :base(message) { }
    }
}
