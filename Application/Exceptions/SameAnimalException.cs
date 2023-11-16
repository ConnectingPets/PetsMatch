namespace Application.Exceptions
{
    public class SameAnimalException : Exception
    {
        public SameAnimalException(string message)
            :base(message) { }
    }
}
