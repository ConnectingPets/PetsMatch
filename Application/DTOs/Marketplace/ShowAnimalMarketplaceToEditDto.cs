using Application.DTOs.Animal;

namespace Application.DTOs.Marketplace
{
    using Domain.Enum;

    public class ShowAnimalMarketplaceToEditDto : ShowAnimalToEditDto
    {
        public AnimalStatus AnimalStatus { get; set; }

        public decimal? Price { get; set; }
    }
}
