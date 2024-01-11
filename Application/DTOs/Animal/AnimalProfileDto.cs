namespace Application.DTOs.Animal
{
    using Application.DTOs.Photo;
    using Domain.Enum;

    public class AnimalProfileDto
    {
        public string AnimalId { get; set; } = null!;

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int Age { get; set; }

        public DateTime? BirthDate { get; set; }

        public bool IsEducated { get; set; }

        public string HealthStatus { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string? SocialMedia { get; set; }

        public double? Weight { get; set; }

        public bool IsHavingValidDocuments { get; set; }

        public string Breed { get; set; } = null!;

        public ICollection<PhotoDto> Photos { get; set; } = null!;
    }
}