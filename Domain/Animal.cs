namespace Domain
{
    using Domain.Enum;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("animal table")]
    public class Animal
    {
        [Comment("animal id")]
        [Key]
        public Guid AnimalId { get; set; }

        [Comment("animal name")]
        public required string Name { get; set; }

        [Comment("animal description")]
        public string? Description { get; set; }

        [Comment("animal age")]
        public required int Age { get; set; }

        [Comment("animal birth date")]
        public DateTime? BirthDate { get; set; }

        [Comment("stores if the animal is educated")]
        public required bool IsEducated { get; set; }

        [Comment("animal photo")]
        public required byte[] Photo { get; set; }

        [Comment("animal health status")]
        public required HealthStatus HealthStatus { get; set; }

        [Comment("animal gender")]
        public required Gender Gender { get; set; }

        [Comment("animal social media")]
        public string? SocialMedia { get; set; }

        [Comment("animal weight")]
        public double? Weight { get; set; }

        [Comment("it stores if the animal has valid documents")]
        public required bool IsHavingValidDocuments { get; set; }

        [Comment("animal breed id")]
        public required int BreedId { get; set; }

        [Comment("animal owner id")]
        public required Guid OwnerId { get; set; }
    }
}