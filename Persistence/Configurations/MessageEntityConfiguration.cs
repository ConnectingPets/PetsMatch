namespace Persistence.Configurations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MessageEntityConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder
                .HasOne(m => m.Animal)
                .WithMany(a => a.Messages)
                .HasForeignKey(m => m.AnimalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(me => me.Match)
                .WithMany(m => m.Messages)
                .HasForeignKey(me => me.MatchId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
