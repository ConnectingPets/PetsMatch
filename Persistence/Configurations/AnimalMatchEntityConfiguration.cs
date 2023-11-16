namespace Persistence.Configuarations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain;

    public class MatchEntityConfiguration : IEntityTypeConfiguration<AnimalMatch>
    {
        public void Configure(EntityTypeBuilder<AnimalMatch> builder)
        {
            builder.ToTable("AnimalMatches");

            builder.HasKey(m => new { m.AnimalId, m.MatchId });

            builder
                .HasOne(m => m.Animal)
                .WithMany(a => a.AnimalMatches)
                .HasForeignKey(m => m.AnimalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(m => m.Match)
                .WithMany(a => a.AnimalMatches)
                .HasForeignKey(m => m.MatchId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
