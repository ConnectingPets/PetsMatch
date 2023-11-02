namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    [Comment("conversation table")]
    public class Conversation
    {
        public Conversation()
        {
            this.Messages = new HashSet<Message>();
        }

        [Comment("conversation id")]
        [Key]
        public required Guid ConversationId { get; set; }

        [Comment("timestamp when the conversation started")]
        public required DateTime StartedOn { get; set; }

        public ICollection<Message> Messages { get; set; } = null!;
    }
}
