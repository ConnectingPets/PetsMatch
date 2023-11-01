namespace Domain
{
    using Microsoft.EntityFrameworkCore;

    [Comment("user passion table")]
    public class UserPassion
    {
        [Comment("passion id")]
        public required int PassionId { get; set; }

        [Comment("user id")]
        public required Guid UserId { get; set; }
    }
}
