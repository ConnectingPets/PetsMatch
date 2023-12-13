namespace Application.DTOs
{
    public class AllAnimalDto
    {
        public required string  Id { get; set; }

        public required string Name { get; set; }

        public required byte[] Photo { get; set; }
    }
}
