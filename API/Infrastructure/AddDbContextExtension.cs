namespace API.Infrastructure
{
    using Microsoft.EntityFrameworkCore;

    using Persistence;

    /// <summary>
    /// This is an extension method for configuration DbContext. 
    /// It gets the connection string from the configuration and adds the db context to the service collection.
    /// </summary>
    public static class AddDbContextExtension
    {
        public static IServiceCollection ConfigurateDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            //string connectionString = configuration.GetConnectionString("DefaultConnection") ??
            //    throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

            string? dbHost = Environment.GetEnvironmentVariable("DB_HOST");
            string? dbName = Environment.GetEnvironmentVariable("DB_NAME");
            string? dbPassword = Environment.GetEnvironmentVariable("DB_SA_PASSWORD");

            string fullConnectionString = $"Data Source={dbHost};Initial Catalog={dbName};User Id=sa;Password={dbPassword};TrustServerCertificate=true;";
            services.AddDbContext<DataContext>(options => options.UseSqlServer(fullConnectionString));

            return services;
        }
    }
}
