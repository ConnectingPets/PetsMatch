namespace Application.DTOs.Marketplace
{
    using Animal;

    public class AllAnimalsForSaleDto : AllAnimalsMarketplaceDto
    {
        public decimal? Price { get; set; }
    }
}
