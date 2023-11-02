namespace Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("match table")]
    public class Match
    {
        [Comment("match animal one id")]
        public required Guid AnimalOneId { get; set; }

        [Comment("match animal one")]
        [ForeignKey(nameof(AnimalOneId))]
        public Animal AnimalOne { get; set; } = null!;

        [Comment("match animal two id")]
        public required Guid AnimalTwoId { get; set; }

        [Comment("match animal one")]
        [ForeignKey(nameof(AnimalTwoId))]
        public Animal AnimalTwo { get; set; } = null!;

        [Comment("timestamp when the match is done")]
        public required DateTime MatchOn { get; set; }
    }
}
