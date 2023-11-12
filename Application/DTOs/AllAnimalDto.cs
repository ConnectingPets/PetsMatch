namespace Application.DTOs
{
    public class AllAnimalDto
    {
        public required string  Id { get; set; }

        public required string Name { get; set; }

        public string? Description { get; set; }

        public required int Age { get; set; }

        public string? BirthDate { get; set; }

        public required bool IsEducated { get; set; }

        public required byte[] Photo { get; set; }

        public required string HealthStatus { get; set; }

        public required string Gender { get; set; }

        public string? SocialMedia { get; set; }

        public double? Weight { get; set; }

        public required bool IsHavingValidDocuments { get; set; }

        public string Breed { get; set; } = null!;

        public string Category { get; set; } = null!;
    }
}
