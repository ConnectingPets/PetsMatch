namespace Domain
{
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using static Common.EntityValidationConstants.Message;
    using static Common.ExceptionMessages.Message;

    [Comment("message table")]
    public class Message
    {
        [Comment("message animal id")]
        public required Guid AnimalId { get; set; }

        [Comment("message animal")]
        [ForeignKey(nameof(AnimalId))]
        public Animal Animal { get; set; } = null!;

        [Comment("message conversation id")]
        public required Guid ConversationId { get; set; }

        [Comment("message conversation")]
        [ForeignKey(nameof(ConversationId))]
        public Conversation Conversation { get; set; } = null!;

        [Comment("message content")]
        [StringLength(ContentMaxLength, ErrorMessage = InvalidContentLength)]
        public required string Content { get; set; }

        [Comment("timestamp when the message is sent")]
        public required DateTime SentOn { get; set; }
    }
}
