namespace Persistence.Configurations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain;

    public class AnimalEntityConfiguration : IEntityTypeConfiguration<Animal>
    {
        public void Configure(EntityTypeBuilder<Animal> builder)
        {
            builder.Property(a => a.CreatedOn)
                .HasDefaultValueSql("GETDATE()");
        }
    }
}
