namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("breed table")]
    public class Breed
    {
        [Comment("breed id")]
        [Key]
        public int BreedId { get; set; }

        [Comment("breed name")]
        public required string Name { get; set; }

        [Comment("category id")]
        public required int CategoryId { get; set; }
    }
}
