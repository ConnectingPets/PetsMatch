namespace Persistence.Configuarations
{
    using Domain;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Metadata.Builders;

    public class UserPassionEntityConfiguration : IEntityTypeConfiguration<UserPassion>
    {
        public void Configure(EntityTypeBuilder<UserPassion> builder)
        {
            builder.ToTable("UsersPassions");

            builder.HasKey(up => new { up.PassionId, up.UserId });

            builder
                .HasOne(up => up.Passion)
                .WithMany(p => p.UsersPassions)
                .HasForeignKey(up => up.PassionId);

            builder
                .HasOne(up => up.User)
                .WithMany(u => u.UsersPassions)
                .HasForeignKey(up => up.UserId);
        }
    }
}
