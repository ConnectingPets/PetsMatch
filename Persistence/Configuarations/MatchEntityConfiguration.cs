namespace Persistence.Configuarations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class MatchEntityConfiguration : IEntityTypeConfiguration<Match>
    {
        public void Configure(EntityTypeBuilder<Match> builder)
        {
            builder.ToTable("Matches");

            builder.HasKey(m => new { m.AnimalOneId, m.AnimalTwoId });

            builder
                .HasOne(m => m.AnimalOne)
                .WithMany(a => a.Matches)
                .HasForeignKey(m => m.AnimalOneId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(m => m.AnimalTwo)
                .WithMany()
                .HasForeignKey(m => m.AnimalTwoId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
