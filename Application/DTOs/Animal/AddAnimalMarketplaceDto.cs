namespace Application.DTOs.Animal
{
    public class AddAnimalMarketplaceDto : AddAnimalDto
    {
        public bool IsForSale { get; set; }
        public decimal? Price { get; set; }
    }
}
