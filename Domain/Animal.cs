namespace Domain
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using static Common.EntityValidationConstants.Animal;
    using static Common.ExceptionMessages.Animal;
    using Domain.Enum;

    [Comment("animal table")]
    public class Animal
    {
        public Animal()
        {
            this.AnimalId = Guid.NewGuid();
            this.SwipesFrom = new HashSet<Swipe>();
            this.SwipesTo = new HashSet<Swipe>();
            this.AnimalMatches = new HashSet<AnimalMatch>();
            this.Messages = new HashSet<Message>();
        }

        [Comment("animal id")]
        [Key]
        public Guid AnimalId { get; set; }

        [Comment("animal name")]
        [StringLength(NameMaxLength, ErrorMessage = InvalidNameLength)]
        public required string Name { get; set; }

        [Comment("animal description")]
        [StringLength(DescriptionMaxLength, ErrorMessage = InvalidDescriptionLength)]
        public string? Description { get; set; }

        [Comment("animal age")]
        [Range(typeof(int), AgeMinValue, AgeMaxValue)]
        public required int Age { get; set; }

        [Comment("animal birth date")]
        public DateTime? BirthDate { get; set; }

        [Comment("animal last modified")]
        public DateTime? LastModified { get; set; }

        [Comment("stores if the animal is educated")]
        public required bool IsEducated { get; set; }

        [Comment("animal photo")]
        public required byte[] Photo { get; set; }

        [Comment("animal health status")]
        [DisplayName("Health Status")]
        public required HealthStatus HealthStatus { get; set; }

        [Comment("animal gender")]
        public required Gender Gender { get; set; }

        [Comment("animal social media")]
        [DisplayName("Social Media")]
        public string? SocialMedia { get; set; }

        [Comment("animal weight")]
        [Range(typeof(double), WeigthMinValue, WeigthMaxValue)]
        public double? Weight { get; set; }

        [Comment("it stores if the animal has valid documents")]
        public required bool IsHavingValidDocuments { get; set; }

        [Comment("animal breed id")]
        public required int BreedId { get; set; }

        [Comment("animal breed")]
        [ForeignKey(nameof(BreedId))]
        public Breed Breed { get; set; } = null!;

        [Comment("animal owner id")]
        public required Guid OwnerId { get; set; }

        [Comment("animal owner")]
        [ForeignKey(nameof(OwnerId))]
        public User Owner { get; set; } = null!;

        public ICollection<Swipe> SwipesTo { get; set; } = null!;

        public ICollection<Swipe> SwipesFrom { get; set; } = null!;  
        
        public ICollection<AnimalMatch> AnimalMatches { get; set; } = null!;

        public ICollection<Message> Messages { get; set; } = null!;
    }
}