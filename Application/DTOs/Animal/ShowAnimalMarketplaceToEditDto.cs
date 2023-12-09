namespace Application.DTOs.Animal
{
    using Domain.Enum;

    public class ShowAnimalMarketplaceToEditDto : ShowAnimalToEditDto
    {
        public AnimalStatus AnimalStatus { get; set; }

        public decimal? Price { get; set; }
    }
}
