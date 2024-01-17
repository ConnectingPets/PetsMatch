namespace Application.DTOs.Marketplace
{
    using Animal;

    public class AllAnimalsForSaleDto : AllAnimalsDto
    {
        public decimal? Price { get; set; }
    }
}
