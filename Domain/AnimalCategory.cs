namespace Domain
{
    using System.ComponentModel.DataAnnotations;

    using Microsoft.EntityFrameworkCore;

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
        [StringLength(NameMaxLength, ErrorMessage = InvalidNameLength)]
        public required string Name { get; set; }

        public ICollection<Breed> Breeds { get; set; } = null!;
    }
}
