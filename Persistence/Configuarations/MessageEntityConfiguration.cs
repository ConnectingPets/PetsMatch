namespace Persistence.Configuarations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain;

    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");

            builder.HasKey(m => new { m.AnimalId, m.ConversationId });

            builder
                .HasOne(m => m.Animal)
                .WithMany(a => a.Messages)
                .HasForeignKey(m => m.AnimalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(m => m.Conversation)
                .WithMany(c => c.Messages)
                .HasForeignKey(m => m.ConversationId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
