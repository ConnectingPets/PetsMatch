namespace Application.DTOs.Swipe
{
    public class AnimalToSwipeDto
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int Age { get; set; }

        public DateTime? BirthDate {  get; set; }

        public bool IsEducated { get; set; }

        public string HealthStatus { get; set; } = null!;

        public string Gender { get; set; } = null!;

        public string? SocialMedia { get; set; }

        public double? Weight { get; set; }

        public bool IsHavingValidDocuments { get; set; }

        public string Breed { get; set; } = null!;

        public string? Photo { get; set; }
    }
}
