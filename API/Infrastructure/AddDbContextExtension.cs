namespace API.Infrastructure
{
    using Microsoft.EntityFrameworkCore;
    using Persistence;

    public static class AddDbContextExtension
    {
        public static IServiceCollection ConfigurateDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            string connectionString = configuration.GetConnectionString("DefaultConnection") ??
                throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            services.AddDbContext<DataContext>(options => options.UseSqlServer(connectionString));

            return services;
        }
    }
}
