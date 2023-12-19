using Application.DTOs.Animal;

namespace Application.DTOs.Marketplace
{
    public class AddAnimalMarketplaceDto : AddAnimalDto
    {
        public bool IsForSale { get; set; }
        public decimal? Price { get; set; }
    }
}
