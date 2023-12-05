namespace Application.DTOs.Animal
{
    using Photo;
    using Domain.Enum;

    public class ShowAnimalToEditDto
    {
        public ShowAnimalToEditDto()
        {
            Photos = new HashSet<PhotoDto>();
        }

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

        public DateTime LastModifiedName { get; set; }

        public DateTime LastModifiedBreed { get; set; }

        public DateTime LastModifiedGender { get; set; }

        public ICollection<PhotoDto> Photos { get; set; }

        public int BreedId { get; set; }

        public string BreedName { get; set; } = null!;

        public int CategoryId { get; set; }
    }
}
