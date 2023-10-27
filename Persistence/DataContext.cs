using Domain;

namespace Persistence
{ 
    //EF To be installed
    public class DataContext : IdentityDbContext<AppUser> 
    {
        public DataContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Match> Matches { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

        }
    }
}