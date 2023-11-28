namespace Application.DTOs
{
    public class AllAnimalDto
    {
        public AllAnimalDto()
        {
            MainPhotos = new HashSet<string>();
        }

        public required string  Id { get; set; }

        public required string Name { get; set; }

        public ICollection<string> MainPhotos { get; set; }
    }
}
