namespace Application.Exceptions.Entity
{
    using static Common.ExceptionMessages.Entity;

    public class InvalidEnumException : Exception
    {
        public InvalidEnumException()
        {

        }

        public InvalidEnumException(string enumName)
            : base(string.Format(InvalidEnum, enumName))
        {

        }
    }
}
