namespace Application.DTOs
{
    using Domain.Enum;

    public class ShowAnimalToEditDto : ShowAnimalToAddDto
    {
        public ShowAnimalToEditDto()
        {
            Breeds = new HashSet<BreedDto>();
            AnimalCategories = new HashSet<AnimalCategoryDto>();
        }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public int Age { get; set; }

        public string? BirthDate { get; set; }

        public bool IsEducated { get; set; }

        public byte[]? Photo { get; set; }

        public HealthStatus HealthStatus { get; set; }

        public Gender Gender { get; set; }

        public string? SocialMedia { get; set; }

        public double? Weight { get; set; }

        public bool IsHavingValidDocuments { get; set; }

        public bool CanEditAll { get; set; }
    }
}
