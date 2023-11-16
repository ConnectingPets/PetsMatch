namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("match table")]
    public class Match
    {
        public Match()
        {
            this.MatchId = Guid.NewGuid();
            this.AnimalMatches = new HashSet<AnimalMatch>();
        }

        [Comment("match id")]
        [Key]
        public Guid MatchId {  get; set; }

        [Comment("timestamp when the match is done")]
        public required DateTime MatchOn { get; set; }

        public ICollection<AnimalMatch> AnimalMatches { get; set; } = null!;
    }
}
