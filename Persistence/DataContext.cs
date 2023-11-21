namespace Persistence
{
    using System.Reflection;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.AspNetCore.Identity;

    using Domain;

    public class DataContext : IdentityDbContext<User, IdentityRole<Guid>, Guid>
    {
        public DataContext(DbContextOptions options)
            : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; } = null!;

        public DbSet<AnimalCategory> AnimalCategories { get; set; } = null!;

        public DbSet<Breed> Breeds { get; set; } = null!;
        
        public DbSet<Passion> Passions { get; set; } = null!;

        public DbSet<Match> Matches { get; set; } = null!;

        public DbSet<Message> Messages { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(DataContext)) ??
                                Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(builder);
        }
    }
}