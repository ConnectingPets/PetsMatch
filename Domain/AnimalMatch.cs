namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Comment("animal match table")]
    public class AnimalMatch
    {
        [Comment("animal id")]
        [Required]
        public Guid AnimalId { get; set; }

        [Comment("animal")]
        [ForeignKey(nameof(AnimalId))]
        public Animal Animal { get; set; } = null!;

        [Comment("match id")]
        [Required]
        public Guid MatchId { get; set; }

        [Comment("match")]
        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = null!;
    }
}
