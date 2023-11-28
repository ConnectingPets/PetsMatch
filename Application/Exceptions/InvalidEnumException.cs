namespace Application.Exceptions
{
    using static Common.ExceptionMessages.Entity;

    public class InvalidEnumException : Exception
    {
        public InvalidEnumException()
        {

        }

        public InvalidEnumException(string enumName) 
            :base(String.Format(InvalidEnum, enumName))
        {

        }
    }
}
