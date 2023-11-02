namespace Persistence
{
    using System.Reflection;

    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;

    using Domain;

    public class DataContext : IdentityDbContext<User>
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Animal> Animals { get; set; } = null!;

        public DbSet<AnimalCategory> AnimalCategories { get; set; } = null!;

        public DbSet<Breed> Breeds { get; set; } = null!;

        public DbSet<Conversation> Conversations { get; set; } = null!;
        
        public DbSet<Passion> Passions { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder builder)
        {
            Assembly assembly = Assembly.GetAssembly(typeof(DataContext)) ??
                                Assembly.GetExecutingAssembly();

            builder.ApplyConfigurationsFromAssembly(assembly);
            base.OnModelCreating(builder);
        }
    }
}