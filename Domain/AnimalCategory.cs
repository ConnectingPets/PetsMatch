namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using static Common.EntityValidationConstants.AnimalCategory;
    using static Common.ExceptionMessages.AnimalCategory;

    [Comment("animal category table")]
    public class AnimalCategory
    {
        public AnimalCategory()
        {
            this.Breeds = new HashSet<Breed>();
        }

        [Comment("animal category id")]
        [Key]
        public int AnimalCategoryId { get; set; }

        [Comment("animal category name")]
        [StringLength(NameMaxLength, MinimumLength = NameMinLength, ErrorMessage = InvalidNameLength)]
        public required string Name { get; set; }

        public ICollection<Breed> Breeds { get; set; } = null!;
    }
}
