namespace API.Infrastructure
{
    using Persistence.Repositories;
    using Application.Service;
    using Application.Service.Interfaces;
    using Application.Swipe;
    public static class AddServicesExtension
    {
        public static IServiceCollection ConfigurateServices(this IServiceCollection services)
        {
            services.AddScoped<IRepository, Repository>();
            services.AddScoped<ISwipeService, SwipeService>();
            services.AddScoped<IMatchService, MatchService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IMessageService, MessageService>();
            services.AddScoped<IProfileService, ProfileService>();
            services.AddScoped<IPhotoService, PhotoService>();
            services.AddSignalR();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(typeof(SwipeAnimal).Assembly));
            return services;
        }
    }
}
