namespace Application.DTOs.Marketplace
{
    using Application.DTOs.Animal;

    public class AllAnimalsMarketplaceDto : AllAnimalsDto
    {
        public string Category { get; set; } = null!;

        public string Breed { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string City { get; set; } = null!;
    }
}
