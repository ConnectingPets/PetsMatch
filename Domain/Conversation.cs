namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("conversation table")]
    public class Conversation
    {
        [Comment("conversation id")]
        [Key]
        public required Guid ConversationId { get; set; }

        [Comment("timestamp when the conversation started")]
        public required DateTime StartedOn { get; set; }
    }
}
