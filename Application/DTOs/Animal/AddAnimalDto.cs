namespace Application.DTOs.Animal
{
    using Photo;

    public class AddAnimalDto : EditAnimalDto
    {
        public MainPhotoDto[] Photos { get; set; } = null!;
    }
}
