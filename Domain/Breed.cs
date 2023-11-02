namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using static Common.EntityValidationConstants.Breed;
    using static Common.ExceptionMessages.Breed;

    [Comment("breed table")]
    public class Breed
    {
        public Breed()
        {
            this.Animals = new HashSet<Animal>();
        }

        [Comment("breed id")]
        [Key]
        public int BreedId { get; set; }

        [Comment("breed name")]
        [StringLength(NameMaxLength, ErrorMessage = InvalidNameLength)]
        public required string Name { get; set; }

        [Comment("animal category id")]
        public required int CategoryId { get; set; }

        [Comment("animal category")]
        [ForeignKey(nameof(CategoryId))]
        public AnimalCategory Category { get; set; } = null!;

        public ICollection<Animal> Animals { get; set; } = null!;
    }
}
