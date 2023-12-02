namespace Application.Exceptions.Entity
{
    using static Common.ExceptionMessages.Entity;

    public class InvalidGuidFormatException : Exception
    {
        public InvalidGuidFormatException()
            : base(InvalidGuidFormat) { }

        public InvalidGuidFormatException(string message)
            : base(message) { }
    }
}
