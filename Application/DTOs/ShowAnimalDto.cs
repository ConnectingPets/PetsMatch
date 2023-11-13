namespace Application.DTOs
{
    using Domain.Enum;

    public class ShowAnimalDto
    {
        public ShowAnimalDto()
        {
            Breeds = new HashSet<BreedDto>();
            AnimalCategories = new HashSet<AnimalCategoryDto>();
            Gender = new HashSet<Gender>();
            HealthStatus = new HashSet<HealthStatus>();
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Age { get; set; }

        public string? BirthDate { get; set; }

        public bool IsEducated { get; set; }

        public byte[]? Photo { get; set; }

        public IEnumerable<HealthStatus> HealthStatus { get; set; }

        public IEnumerable<Gender> Gender { get; set; }

        public string? SocialMedia { get; set; }

        public double? Weight { get; set; }

        public bool IsHavingValidDocuments { get; set; }

        public IEnumerable<BreedDto> Breeds { get; set; } = null!;

        public IEnumerable<AnimalCategoryDto> AnimalCategories { get; set; } = null!;
    }
}
