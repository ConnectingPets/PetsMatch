namespace API.Infrastructure
{
    using Application.Service;
    using Application.Service.Interfaces;
    using Persistence.Repositories;

    public static class AddServicesExtenstion
    {
        public static IServiceCollection ConfigurateServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ISwipeService, SwipeService>();
            services.AddScoped<IMatchService, MatchService>();

            return services;
        }
    }
}
