namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("animal category table")]
    public class AnimalCategory
    {
        [Comment("animal category id")]
        [Key]
        public int CategoryId { get; set; }

        [Comment("animal category name")]
        public required string Name { get; set; }
    }
}
