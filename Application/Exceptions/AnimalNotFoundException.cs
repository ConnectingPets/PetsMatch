namespace Application.Exceptions
{
    using static Common.ExceptionMessages.Animal;

    public class AnimalNotFoundException : Exception
    {
        public AnimalNotFoundException() 
            :base(AnimalNotFound) { }

        public AnimalNotFoundException(string message)
            :base(message) { }
    }
}
