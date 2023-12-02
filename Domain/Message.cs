namespace Domain
{
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    using Microsoft.EntityFrameworkCore;

    using static Common.EntityValidationConstants.Message;
    using static Common.ExceptionMessages.Message;

    [Comment("message table")]
    public class Message
    {
        public Message()
        {
            this.MessageId = Guid.NewGuid();
        }

        [Comment("message id")]
        [Key]
        public Guid MessageId { get; set; }

        [Comment("message animal id")]
        public required Guid AnimalId { get; set; }

        [Comment("message animal")]
        [ForeignKey(nameof(AnimalId))]
        public Animal Animal { get; set; } = null!;

        [Comment("message match id")]
        public required Guid MatchId { get; set; }

        [Comment("message conversation")]
        [ForeignKey(nameof(MatchId))]
        public Match Match { get; set; } = null!;

        [Comment("message content")]
        [StringLength(ContentMaxLength, ErrorMessage = InvalidContentLength)]
        public required string Content { get; set; }

        [Comment("timestamp when the message is sent")]
        public required DateTime SentOn { get; set; }
    }
}
