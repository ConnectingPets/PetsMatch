namespace Application.Exceptions.Animal
{
    using static Common.ExceptionMessages.Animal;

    public class SameAnimalException : Exception
    {
        public SameAnimalException()
            : base(SameAnimal) { }

        public SameAnimalException(string message)
            : base(message) { }
    }
}
