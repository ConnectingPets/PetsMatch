namespace API.Infrastructure
{
	using Application.Service;
	using Application.Service.Interfaces;
	using Application.Swipe;
	using Persistence.Repositories;
    public static class AddServicesExtension
    {
        public static IServiceCollection ConfigurateServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ISwipeService, SwipeService>();
            services.AddScoped<IMatchService, MatchService>();

	 services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SwipeAnimal).Assembly));
            return services;
        }
    }
}
