namespace Domain
{
    using Microsoft.EntityFrameworkCore;

    [Comment("message table")]
    public class Message
    {
        [Comment("message animal id")]
        public required Guid AnimalId { get; set; }

        [Comment("message conversation id")]
        public required Guid ConversationId { get; set; }

        [Comment("message content")]
        public required string Content { get; set; }

        [Comment("timestamp when the message is sent")]
        public required DateTime SentOn { get; set; }
    }
}
