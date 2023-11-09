namespace API.Infrastructure
{
    using Persistence.Repositories;

    public static class AddServicesExtenstion
    {
        public static IServiceCollection ConfigurateServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();

            return services;
        }
    }
}
