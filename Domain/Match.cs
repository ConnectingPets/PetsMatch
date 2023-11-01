namespace Domain
{
    using Microsoft.EntityFrameworkCore;

    [Comment("match table")]
    public class Match
    {
        [Comment("match animal one id")]
        public required Guid AnimalOneId { get; set; }

        [Comment("match animal two id")]
        public required Guid AnimalTwoId { get; set; }

        [Comment("timestamp when the match is done")]
        public required DateTime MatchOn { get; set; }
    }
}
