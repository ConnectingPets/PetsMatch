namespace Application.DTOs.Marketplace
{
    using Animal;

    public class AddAnimalMarketplaceDto : AddAnimalDto
    {
        public bool IsForSale { get; set; }
        public decimal? Price { get; set; }
    }
}
