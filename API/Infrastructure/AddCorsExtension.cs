namespace API.Infrastructure
{
    public static class AddCorsExtension
    {
        public static IServiceCollection ConfigurateCors(this IServiceCollection services, IConfiguration configuration)
        {
            string reactBaseUrl = configuration.GetValue<string>("ReactApp:BaseUrl") ??
                throw new InvalidOperationException("The react base url is not found.");

            services.AddCors(options =>
            {
                options.AddPolicy("ReactPolicy", builder =>
                {
                    builder.WithOrigins(reactBaseUrl)
                        .AllowAnyHeader()
                        .AllowAnyMethod()
                        .AllowCredentials();
                });
            });

            return services;
        }
    }
}
