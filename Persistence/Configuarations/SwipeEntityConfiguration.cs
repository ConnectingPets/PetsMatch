namespace Persistence.Configuarations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class SwipeEntityConfiguration : IEntityTypeConfiguration<Swipe>
    {
        public void Configure(EntityTypeBuilder<Swipe> builder)
        {
            builder.ToTable("Swipes");

            builder.HasKey(s => new { s.SwiperAnimalId, s.SwipeeAnimalId });

            builder
                .HasOne(s => s.SwiperAnimal)
                .WithMany(a => a.Swipes)
                .HasForeignKey(s => s.SwiperAnimalId)
                .OnDelete(DeleteBehavior.NoAction);

            builder
                .HasOne(s => s.SwipeeAnimal)
                .WithMany()
                .HasForeignKey(s => s.SwipeeAnimalId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
