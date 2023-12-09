namespace Application.DTOs.Animal
{
    using Domain.Enum;

    public class EditAnimalMarketplaceDto : EditAnimalDto
    {
        public AnimalStatus AnimalStatus { get; set; }

        public decimal? Price { get; set; }
    }
}
