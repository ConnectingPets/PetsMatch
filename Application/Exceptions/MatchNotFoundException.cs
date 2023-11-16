namespace Application.Exceptions
{
    public class MatchNotFoundException : Exception
    {
        public MatchNotFoundException(string message)
            : base(message) { }
    }
}
