namespace Domain
{
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    [Comment("user passion table")]
    public class UserPassion
    {
        [Comment("passion id")]
        public required int PassionId { get; set; }

        [Comment("user passion")]
        [ForeignKey(nameof(PassionId))]
        public Passion Passion { get; set; } = null!;

        [Comment("user id")]
        public required Guid UserId { get; set; }

        [Comment("user")]
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;
    }
}
