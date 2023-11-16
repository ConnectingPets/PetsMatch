namespace Application.Exceptions
{
    public class AnimalNotFoundException : Exception
    {
        public AnimalNotFoundException(string message)
            :base(message) { }
    }
}
