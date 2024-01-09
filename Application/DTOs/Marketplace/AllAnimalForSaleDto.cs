namespace Application.DTOs.Marketplace
{
    using Animal;

    public class AllAnimalForSaleDto : AllAnimalDto
    {
        public decimal? Price { get; set; }
    }
}
