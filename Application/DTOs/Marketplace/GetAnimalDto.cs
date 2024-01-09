namespace Application.DTOs.Marketplace
{
    using Domain.Enum;
    using Photo;

    public class GetAnimalDto
    {
        public GetAnimalDto()
        {
            Photos = new HashSet<PhotoDto>();
        }

        public string UserName { get; set; } = null!;

        public string? UserEmail { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Age { get; set; }

        public string? BirthDate { get; set; }

        public bool IsEducated { get; set; }

        public HealthStatus HealthStatus { get; set; }

        public Gender Gender { get; set; }

        public string? SocialMedia { get; set; }

        public double? Weight { get; set; }

        public bool IsHavingValidDocuments { get; set; }

        public ICollection<PhotoDto> Photos { get; set; }

        public string BreedName { get; set; } = null!;

        public decimal? Price { get; set; }
    }
}
