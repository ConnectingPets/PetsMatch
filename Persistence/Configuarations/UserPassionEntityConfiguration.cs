namespace Persistence.Configuarations
{
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    using Domain;

    public class UserPassionEntityConfiguration : IEntityTypeConfiguration<UserPassion>
    {
        public void Configure(EntityTypeBuilder<UserPassion> builder)
        {
            builder.ToTable("UsersPassions");

            builder.HasKey(up => new { up.PassionId, up.UserId });
        }
    }
}
