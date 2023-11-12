namespace API.Infrastructure
{
    using Application.Animal;
    using Persistence.Repositories;

    public static class AddServicesExtenstion
    {
        public static IServiceCollection ConfigurateServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(AddAnimal).Assembly));

            return services;
        }
    }
}
